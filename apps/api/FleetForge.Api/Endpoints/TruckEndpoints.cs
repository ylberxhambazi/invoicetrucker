using FleetForge.Api.Contracts.Common;
using FleetForge.Api.Contracts.Trucks;
using FleetForge.Api.Domain.Entities;
using FleetForge.Api.Domain.Enums;
using FleetForge.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Endpoints;

public static class TruckEndpoints
{
    private static readonly HashSet<string> SortFields =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "name",
            "registrationNumber",
            "year",
            "status",
            "currentMileage",
            "insuranceExpiration"
        };

    public static IEndpointRouteBuilder MapTruckEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/trucks").WithTags("Trucks");

        group.MapGet("/", GetTrucks)
            .WithName("GetTrucks")
            .Produces<PaginatedResponse<TruckSummaryResponse>>()
            .ProducesValidationProblem();
        group.MapGet("/{id:guid}", GetTruck)
            .WithName("GetTruck")
            .Produces<TruckDetailResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> GetTrucks(
        [AsParameters] TruckQuery request,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var errors = QueryValidation.Validate(request, SortFields);
        EnumQuery.TryParse<TruckStatus>(request.Status, "status", errors, out var status);

        if (request.Year is < 1990 or > 2100)
        {
            errors["year"] = ["Year must be between 1990 and 2100."];
        }

        if (errors.Count > 0)
        {
            return EndpointResults.InvalidQuery(errors);
        }

        IQueryable<Truck> query = dbContext.Trucks.AsNoTracking();

        if (status.HasValue)
        {
            query = query.Where(truck => truck.Status == status.Value);
        }

        if (request.Year.HasValue)
        {
            query = query.Where(truck => truck.Year == request.Year.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            if (dbContext.Database.IsNpgsql())
            {
                var pattern = SearchPattern.Create(search);
                query = query.Where(truck =>
                    EF.Functions.ILike(
                        truck.RegistrationNumber,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || EF.Functions.ILike(
                        truck.Make,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || EF.Functions.ILike(
                        truck.Model,
                        pattern,
                        SearchPattern.EscapeCharacter));
            }
            else
            {
                var normalized = search.ToLowerInvariant();
                query = query.Where(truck =>
                    truck.RegistrationNumber.ToLower().Contains(normalized)
                    || truck.Make.ToLower().Contains(normalized)
                    || truck.Model.ToLower().Contains(normalized));
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
            .ThenBy(truck => truck.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(truck => new TruckSummaryResponse(
                truck.Id,
                truck.RegistrationNumber,
                truck.Make,
                truck.Model,
                truck.Year,
                truck.Status.ToString(),
                truck.AssignedDriver == null ? null : truck.AssignedDriver.FullName,
                truck.CurrentMileage,
                truck.InsuranceExpiration,
                truck.TechnicalInspectionExpiration))
            .ToListAsync(cancellationToken);

        return Results.Ok(
            PaginatedResponse<TruckSummaryResponse>.Create(
                items,
                page,
                pageSize,
                totalCount));
    }

    private static async Task<IResult> GetTruck(
        Guid id,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var truck = await dbContext.Trucks
            .AsNoTracking()
            .Where(item => item.Id == id)
            .Select(item => new TruckDetailResponse(
                item.Id,
                item.RegistrationNumber,
                item.Make,
                item.Model,
                item.Year,
                item.Status.ToString(),
                item.CurrentMileage,
                item.InsuranceExpiration,
                item.TechnicalInspectionExpiration,
                item.AssignedDriver == null
                    ? null
                    : new AssignedDriverResponse(
                        item.AssignedDriver.Id,
                        item.AssignedDriver.FullName,
                        item.AssignedDriver.Phone,
                        item.AssignedDriver.Status.ToString()),
                item.Expenses.Sum(expense => (decimal?)expense.Amount) ?? 0m,
                item.Documents.Count,
                item.CreatedAtUtc,
                item.UpdatedAtUtc))
            .SingleOrDefaultAsync(cancellationToken);

        return truck is null
            ? EndpointResults.NotFound("Truck", id)
            : Results.Ok(truck);
    }

    private static IOrderedQueryable<Truck> Order(
        IQueryable<Truck> query,
        string sortBy,
        bool descending) =>
        (sortBy.ToLowerInvariant(), descending) switch
        {
            ("registrationnumber", true) =>
                query.OrderByDescending(truck => truck.RegistrationNumber),
            ("registrationnumber", false) =>
                query.OrderBy(truck => truck.RegistrationNumber),
            ("year", true) => query.OrderByDescending(truck => truck.Year),
            ("year", false) => query.OrderBy(truck => truck.Year),
            ("status", true) => query.OrderByDescending(truck => truck.Status),
            ("status", false) => query.OrderBy(truck => truck.Status),
            ("currentmileage", true) =>
                query.OrderByDescending(truck => truck.CurrentMileage),
            ("currentmileage", false) => query.OrderBy(truck => truck.CurrentMileage),
            ("insuranceexpiration", true) =>
                query.OrderByDescending(truck => truck.InsuranceExpiration),
            ("insuranceexpiration", false) =>
                query.OrderBy(truck => truck.InsuranceExpiration),
            ("name", true) => query
                .OrderByDescending(truck => truck.Make)
                .ThenByDescending(truck => truck.Model),
            _ => query
                .OrderBy(truck => truck.Make)
                .ThenBy(truck => truck.Model)
        };
}
