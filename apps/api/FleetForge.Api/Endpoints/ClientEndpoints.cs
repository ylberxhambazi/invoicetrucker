using FleetForge.Api.Contracts.Clients;
using FleetForge.Api.Contracts.Common;
using FleetForge.Api.Domain.Entities;
using FleetForge.Api.Domain.Enums;
using FleetForge.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Endpoints;

public static class ClientEndpoints
{
    private static readonly HashSet<string> SortFields =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "name",
            "country",
            "totalBilled",
            "activeInvoices"
        };

    public static IEndpointRouteBuilder MapClientEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/clients").WithTags("Clients");

        group.MapGet("/", GetClients)
            .WithName("GetClients")
            .Produces<PaginatedResponse<ClientSummaryResponse>>()
            .ProducesValidationProblem();
        group.MapGet("/{id:guid}", GetClient)
            .WithName("GetClient")
            .Produces<ClientDetailResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> GetClients(
        [AsParameters] ClientQuery request,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var errors = QueryValidation.Validate(request, SortFields);

        if (request.Country?.Length > 80)
        {
            errors["country"] = ["Country cannot exceed 80 characters."];
        }

        if (errors.Count > 0)
        {
            return EndpointResults.InvalidQuery(errors);
        }

        IQueryable<Client> query = dbContext.Clients.AsNoTracking();

        if (request.IsActive.HasValue)
        {
            query = query.Where(client => client.IsActive == request.IsActive.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Country))
        {
            var country = request.Country.Trim();
            query = dbContext.Database.IsNpgsql()
                ? query.Where(client => EF.Functions.ILike(client.Country, country))
                : query.Where(client => client.Country.ToLower() == country.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            if (dbContext.Database.IsNpgsql())
            {
                var pattern = SearchPattern.Create(search);
                query = query.Where(client =>
                    EF.Functions.ILike(
                        client.CompanyName,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || EF.Functions.ILike(
                        client.ContactPerson,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || EF.Functions.ILike(
                        client.Email,
                        pattern,
                        SearchPattern.EscapeCharacter));
            }
            else
            {
                var normalized = search.ToLowerInvariant();
                query = query.Where(client =>
                    client.CompanyName.ToLower().Contains(normalized)
                    || client.ContactPerson.ToLower().Contains(normalized)
                    || client.Email.ToLower().Contains(normalized));
            }
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? 20;
        var descending = request.SortDirection?.Equals(
            "desc",
            StringComparison.OrdinalIgnoreCase) == true;
        var orderedQuery = Order(query, request.SortBy ?? "name", descending);

        var items = await orderedQuery
            .ThenBy(client => client.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(client => new ClientSummaryResponse(
                client.Id,
                client.CompanyName,
                client.ContactPerson,
                client.Country,
                client.Email,
                client.Phone,
                client.Invoices.Count(invoice =>
                    invoice.Status == InvoiceStatus.Draft
                    || invoice.Status == InvoiceStatus.Sent
                    || invoice.Status == InvoiceStatus.Overdue),
                client.Invoices.Sum(invoice => (decimal?)invoice.TotalAmount) ?? 0m,
                client.IsActive))
            .ToListAsync(cancellationToken);

        return Results.Ok(
            PaginatedResponse<ClientSummaryResponse>.Create(
                items,
                page,
                pageSize,
                totalCount));
    }

    private static async Task<IResult> GetClient(
        Guid id,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients
            .AsNoTracking()
            .Where(item => item.Id == id)
            .Select(item => new ClientDetailResponse(
                item.Id,
                item.CompanyName,
                item.ContactPerson,
                item.Country,
                item.Email,
                item.Phone,
                item.IsActive,
                item.Invoices.Count(invoice =>
                    invoice.Status == InvoiceStatus.Draft
                    || invoice.Status == InvoiceStatus.Sent
                    || invoice.Status == InvoiceStatus.Overdue),
                item.Invoices.Sum(invoice => (decimal?)invoice.TotalAmount) ?? 0m,
                item.Invoices
                    .OrderByDescending(invoice => invoice.IssueDate)
                    .ThenBy(invoice => invoice.Id)
                    .Take(5)
                    .Select(invoice => new ClientInvoiceResponse(
                        invoice.Id,
                        invoice.InvoiceNumber,
                        invoice.IssueDate,
                        invoice.DueDate,
                        invoice.TotalAmount,
                        invoice.Currency,
                        invoice.Status.ToString()))
                    .ToList(),
                item.CreatedAtUtc,
                item.UpdatedAtUtc))
            .SingleOrDefaultAsync(cancellationToken);

        return client is null
            ? EndpointResults.NotFound("Client", id)
            : Results.Ok(client);
    }

    private static IOrderedQueryable<Client> Order(
        IQueryable<Client> query,
        string sortBy,
        bool descending) =>
        (sortBy.ToLowerInvariant(), descending) switch
        {
            ("country", true) => query.OrderByDescending(client => client.Country),
            ("country", false) => query.OrderBy(client => client.Country),
            ("totalbilled", true) => query.OrderByDescending(client =>
                client.Invoices.Sum(invoice => (decimal?)invoice.TotalAmount) ?? 0m),
            ("totalbilled", false) => query.OrderBy(client =>
                client.Invoices.Sum(invoice => (decimal?)invoice.TotalAmount) ?? 0m),
            ("activeinvoices", true) => query.OrderByDescending(client =>
                client.Invoices.Count(invoice => invoice.Status != InvoiceStatus.Paid)),
            ("activeinvoices", false) => query.OrderBy(client =>
                client.Invoices.Count(invoice => invoice.Status != InvoiceStatus.Paid)),
            ("name", true) => query.OrderByDescending(client => client.CompanyName),
            _ => query.OrderBy(client => client.CompanyName)
        };
}
