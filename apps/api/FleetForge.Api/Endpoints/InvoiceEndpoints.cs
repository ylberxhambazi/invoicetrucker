using FleetForge.Api.Contracts.Common;
using FleetForge.Api.Contracts.Invoices;
using FleetForge.Api.Domain.Entities;
using FleetForge.Api.Domain.Enums;
using FleetForge.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Endpoints;

public static class InvoiceEndpoints
{
    private static readonly HashSet<string> SortFields =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "name",
            "invoiceNumber",
            "client",
            "issueDate",
            "dueDate",
            "amount",
            "status"
        };

    public static IEndpointRouteBuilder MapInvoiceEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/invoices").WithTags("Invoices");

        group.MapGet("/", GetInvoices)
            .WithName("GetInvoices")
            .Produces<PaginatedResponse<InvoiceSummaryResponse>>()
            .ProducesValidationProblem();
        group.MapGet("/{id:guid}", GetInvoice)
            .WithName("GetInvoice")
            .Produces<InvoiceDetailResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> GetInvoices(
        [AsParameters] InvoiceQuery request,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var errors = QueryValidation.Validate(request, SortFields);
        EnumQuery.TryParse<InvoiceStatus>(request.Status, "status", errors, out var status);

        if (request.IssueDateFrom.HasValue
            && request.IssueDateTo.HasValue
            && request.IssueDateFrom > request.IssueDateTo)
        {
            errors["issueDateTo"] = ["Issue date to must be on or after issue date from."];
        }

        if (errors.Count > 0)
        {
            return EndpointResults.InvalidQuery(errors);
        }

        IQueryable<Invoice> query = dbContext.Invoices.AsNoTracking();

        if (status.HasValue)
        {
            query = query.Where(invoice => invoice.Status == status.Value);
        }

        if (request.ClientId.HasValue)
        {
            query = query.Where(invoice => invoice.ClientId == request.ClientId.Value);
        }

        if (request.IssueDateFrom.HasValue)
        {
            query = query.Where(invoice => invoice.IssueDate >= request.IssueDateFrom.Value);
        }

        if (request.IssueDateTo.HasValue)
        {
            query = query.Where(invoice => invoice.IssueDate <= request.IssueDateTo.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            if (dbContext.Database.IsNpgsql())
            {
                var pattern = SearchPattern.Create(search);
                query = query.Where(invoice =>
                    EF.Functions.ILike(
                        invoice.InvoiceNumber,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || EF.Functions.ILike(
                        invoice.Client.CompanyName,
                        pattern,
                        SearchPattern.EscapeCharacter));
            }
            else
            {
                var normalized = search.ToLowerInvariant();
                query = query.Where(invoice =>
                    invoice.InvoiceNumber.ToLower().Contains(normalized)
                    || invoice.Client.CompanyName.ToLower().Contains(normalized));
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
            .ThenBy(invoice => invoice.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(invoice => new InvoiceSummaryResponse(
                invoice.Id,
                invoice.InvoiceNumber,
                invoice.ClientId,
                invoice.Client.CompanyName,
                invoice.Truck == null ? null : invoice.Truck.RegistrationNumber,
                invoice.IssueDate,
                invoice.DueDate,
                invoice.TotalAmount,
                invoice.Currency,
                invoice.Status.ToString()))
            .ToListAsync(cancellationToken);

        return Results.Ok(
            PaginatedResponse<InvoiceSummaryResponse>.Create(
                items,
                page,
                pageSize,
                totalCount));
    }

    private static async Task<IResult> GetInvoice(
        Guid id,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var invoice = await dbContext.Invoices
            .AsNoTracking()
            .Where(item => item.Id == id)
            .Select(item => new InvoiceDetailResponse(
                item.Id,
                item.InvoiceNumber,
                new InvoiceClientResponse(
                    item.Client.Id,
                    item.Client.CompanyName,
                    item.Client.ContactPerson,
                    item.Client.Email,
                    item.Client.Country),
                item.Truck == null
                    ? null
                    : new InvoiceTruckResponse(
                        item.Truck.Id,
                        item.Truck.RegistrationNumber,
                        item.Truck.Make,
                        item.Truck.Model),
                item.IssueDate,
                item.DueDate,
                item.TotalAmount,
                item.Currency,
                item.Status.ToString(),
                item.PaidAtUtc,
                item.Notes,
                item.Items
                    .OrderBy(invoiceItem => invoiceItem.Id)
                    .Select(invoiceItem => new InvoiceItemResponse(
                        invoiceItem.Id,
                        invoiceItem.Description,
                        invoiceItem.Quantity,
                        invoiceItem.UnitPrice,
                        invoiceItem.LineTotal))
                    .ToList(),
                item.CreatedAtUtc,
                item.UpdatedAtUtc))
            .SingleOrDefaultAsync(cancellationToken);

        return invoice is null
            ? EndpointResults.NotFound("Invoice", id)
            : Results.Ok(invoice);
    }

    private static IOrderedQueryable<Invoice> Order(
        IQueryable<Invoice> query,
        string sortBy,
        bool descending) =>
        (sortBy.ToLowerInvariant(), descending) switch
        {
            ("invoicenumber", true) =>
                query.OrderByDescending(invoice => invoice.InvoiceNumber),
            ("invoicenumber", false) =>
                query.OrderBy(invoice => invoice.InvoiceNumber),
            ("client", true) =>
                query.OrderByDescending(invoice => invoice.Client.CompanyName),
            ("client", false) => query.OrderBy(invoice => invoice.Client.CompanyName),
            ("issuedate", false) => query.OrderBy(invoice => invoice.IssueDate),
            ("duedate", true) => query.OrderByDescending(invoice => invoice.DueDate),
            ("duedate", false) => query.OrderBy(invoice => invoice.DueDate),
            ("amount", true) => query.OrderByDescending(invoice => invoice.TotalAmount),
            ("amount", false) => query.OrderBy(invoice => invoice.TotalAmount),
            ("status", true) => query.OrderByDescending(invoice => invoice.Status),
            ("status", false) => query.OrderBy(invoice => invoice.Status),
            ("name", true) or ("issuedate", true) =>
                query.OrderByDescending(invoice => invoice.IssueDate),
            _ => query.OrderBy(invoice => invoice.IssueDate)
        };
}
