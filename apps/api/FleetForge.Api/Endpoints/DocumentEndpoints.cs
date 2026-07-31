using FleetForge.Api.Contracts.Common;
using FleetForge.Api.Contracts.Documents;
using FleetForge.Api.Domain.Entities;
using FleetForge.Api.Domain.Enums;
using FleetForge.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Endpoints;

public static class DocumentEndpoints
{
    private static readonly HashSet<string> SortFields =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "name",
            "type",
            "issuedDate",
            "expirationDate"
        };

    public static IEndpointRouteBuilder MapDocumentEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/documents", GetDocuments)
            .WithTags("Documents")
            .WithName("GetDocuments")
            .Produces<PaginatedResponse<DocumentResponse>>()
            .ProducesValidationProblem();

        return endpoints;
    }

    private static async Task<IResult> GetDocuments(
        [AsParameters] DocumentQuery request,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var errors = QueryValidation.Validate(request, SortFields);
        EnumQuery.TryParse<DocumentType>(request.Type, "type", errors, out var type);
        var expiration = request.Expiration?.Trim().ToLowerInvariant();

        if (expiration is not null
            && expiration is not ("expired" or "expiring" or "valid"))
        {
            errors["expiration"] =
            [
                "Expiration must be one of: expired, expiring, valid."
            ];
        }

        if (errors.Count > 0)
        {
            return EndpointResults.InvalidQuery(errors);
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var expiringBefore = today.AddDays(60);
        IQueryable<Document> query = dbContext.Documents.AsNoTracking();

        if (type.HasValue)
        {
            query = query.Where(document => document.Type == type.Value);
        }

        query = expiration switch
        {
            "expired" => query.Where(document =>
                document.ExpirationDate.HasValue
                && document.ExpirationDate.Value < today),
            "expiring" => query.Where(document =>
                document.ExpirationDate.HasValue
                && document.ExpirationDate.Value >= today
                && document.ExpirationDate.Value <= expiringBefore),
            "valid" => query.Where(document =>
                !document.ExpirationDate.HasValue
                || document.ExpirationDate.Value > expiringBefore),
            _ => query
        };

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            if (dbContext.Database.IsNpgsql())
            {
                var pattern = SearchPattern.Create(search);
                query = query.Where(document =>
                    EF.Functions.ILike(
                        document.Name,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || EF.Functions.ILike(
                        document.ReferenceNumber,
                        pattern,
                        SearchPattern.EscapeCharacter));
            }
            else
            {
                var normalized = search.ToLowerInvariant();
                query = query.Where(document =>
                    document.Name.ToLower().Contains(normalized)
                    || document.ReferenceNumber.ToLower().Contains(normalized));
            }
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? 20;
        var descending = request.SortDirection?.Equals(
            "desc",
            StringComparison.OrdinalIgnoreCase) == true;
        var orderedQuery = Order(query, request.SortBy ?? "name", descending);

        var records = await orderedQuery
            .ThenBy(document => document.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(document => new
            {
                document.Id,
                document.Type,
                document.Name,
                document.ReferenceNumber,
                document.IssuedDate,
                document.ExpirationDate,
                TruckId = document.TruckId,
                TruckName = document.Truck == null
                    ? null
                    : document.Truck.RegistrationNumber,
                DriverId = document.DriverId,
                DriverName = document.Driver == null ? null : document.Driver.FullName,
                ClientId = document.ClientId,
                ClientName = document.Client == null
                    ? null
                    : document.Client.CompanyName
            })
            .ToListAsync(cancellationToken);

        var items = records.Select(document =>
        {
            var owner = document.TruckId.HasValue
                ? new DocumentOwnerResponse(
                    "Truck",
                    document.TruckId.Value,
                    document.TruckName!)
                : document.DriverId.HasValue
                    ? new DocumentOwnerResponse(
                        "Driver",
                        document.DriverId.Value,
                        document.DriverName!)
                    : new DocumentOwnerResponse(
                        "Client",
                        document.ClientId!.Value,
                        document.ClientName!);

            return new DocumentResponse(
                document.Id,
                document.Type.ToString(),
                document.Name,
                document.ReferenceNumber,
                document.IssuedDate,
                document.ExpirationDate,
                GetExpirationStatus(document.ExpirationDate, today, expiringBefore),
                owner);
        }).ToList();

        return Results.Ok(
            PaginatedResponse<DocumentResponse>.Create(
                items,
                page,
                pageSize,
                totalCount));
    }

    private static string GetExpirationStatus(
        DateOnly? expirationDate,
        DateOnly today,
        DateOnly expiringBefore) =>
        expirationDate switch
        {
            null => "NoExpiration",
            var value when value < today => "Expired",
            var value when value <= expiringBefore => "Expiring",
            _ => "Valid"
        };

    private static IOrderedQueryable<Document> Order(
        IQueryable<Document> query,
        string sortBy,
        bool descending) =>
        (sortBy.ToLowerInvariant(), descending) switch
        {
            ("type", true) => query.OrderByDescending(document => document.Type),
            ("type", false) => query.OrderBy(document => document.Type),
            ("issueddate", true) =>
                query.OrderByDescending(document => document.IssuedDate),
            ("issueddate", false) =>
                query.OrderBy(document => document.IssuedDate),
            ("expirationdate", true) =>
                query.OrderByDescending(document => document.ExpirationDate),
            ("expirationdate", false) =>
                query.OrderBy(document => document.ExpirationDate),
            ("name", true) => query.OrderByDescending(document => document.Name),
            _ => query.OrderBy(document => document.Name)
        };
}
