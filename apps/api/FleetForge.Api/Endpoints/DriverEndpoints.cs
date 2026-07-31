using FleetForge.Api.Contracts.Common;
using FleetForge.Api.Contracts.Drivers;
using FleetForge.Api.Domain.Entities;
using FleetForge.Api.Domain.Enums;
using FleetForge.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Endpoints;

public static class DriverEndpoints
{
    private static readonly HashSet<string> SortFields =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "name",
            "licenceExpiration",
            "status",
            "completedTrips"
        };

    public static IEndpointRouteBuilder MapDriverEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/drivers").WithTags("Drivers");

        group.MapGet("/", GetDrivers)
            .WithName("GetDrivers")
            .Produces<PaginatedResponse<DriverSummaryResponse>>()
            .ProducesValidationProblem();
        group.MapGet("/{id:guid}", GetDriver)
            .WithName("GetDriver")
            .Produces<DriverDetailResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> GetDrivers(
        [AsParameters] DriverQuery request,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var errors = QueryValidation.Validate(request, SortFields);
        EnumQuery.TryParse<DriverStatus>(request.Status, "status", errors, out var status);

        if (errors.Count > 0)
        {
            return EndpointResults.InvalidQuery(errors);
        }

        IQueryable<Driver> query = dbContext.Drivers.AsNoTracking();

        if (status.HasValue)
        {
            query = query.Where(driver => driver.Status == status.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            if (dbContext.Database.IsNpgsql())
            {
                var pattern = SearchPattern.Create(search);
                query = query.Where(driver =>
                    EF.Functions.ILike(
                        driver.FullName,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || EF.Functions.ILike(
                        driver.LicenceNumber,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || EF.Functions.ILike(
                        driver.Email,
                        pattern,
                        SearchPattern.EscapeCharacter));
            }
            else
            {
                var normalized = search.ToLowerInvariant();
                query = query.Where(driver =>
                    driver.FullName.ToLower().Contains(normalized)
                    || driver.LicenceNumber.ToLower().Contains(normalized)
                    || driver.Email.ToLower().Contains(normalized));
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
            .ThenBy(driver => driver.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(driver => new DriverSummaryResponse(
                driver.Id,
                driver.FullName,
                driver.Phone,
                driver.LicenceNumber,
                driver.LicenceExpiration,
                driver.AssignedTruck == null
                    ? null
                    : driver.AssignedTruck.RegistrationNumber,
                driver.Status.ToString(),
                driver.CompletedTrips))
            .ToListAsync(cancellationToken);

        return Results.Ok(
            PaginatedResponse<DriverSummaryResponse>.Create(
                items,
                page,
                pageSize,
                totalCount));
    }

    private static async Task<IResult> GetDriver(
        Guid id,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var driver = await dbContext.Drivers
            .AsNoTracking()
            .Where(item => item.Id == id)
            .Select(item => new DriverDetailResponse(
                item.Id,
                item.FullName,
                item.Phone,
                item.Email,
                item.LicenceNumber,
                item.LicenceExpiration,
                item.Status.ToString(),
                item.CompletedTrips,
                item.AssignedTruck == null
                    ? null
                    : new AssignedTruckResponse(
                        item.AssignedTruck.Id,
                        item.AssignedTruck.RegistrationNumber,
                        item.AssignedTruck.Make,
                        item.AssignedTruck.Model,
                        item.AssignedTruck.Status.ToString()),
                item.Documents.Count,
                item.CreatedAtUtc,
                item.UpdatedAtUtc))
            .SingleOrDefaultAsync(cancellationToken);

        return driver is null
            ? EndpointResults.NotFound("Driver", id)
            : Results.Ok(driver);
    }

    private static IOrderedQueryable<Driver> Order(
        IQueryable<Driver> query,
        string sortBy,
        bool descending) =>
        (sortBy.ToLowerInvariant(), descending) switch
        {
            ("licenceexpiration", true) =>
                query.OrderByDescending(driver => driver.LicenceExpiration),
            ("licenceexpiration", false) =>
                query.OrderBy(driver => driver.LicenceExpiration),
            ("status", true) => query.OrderByDescending(driver => driver.Status),
            ("status", false) => query.OrderBy(driver => driver.Status),
            ("completedtrips", true) =>
                query.OrderByDescending(driver => driver.CompletedTrips),
            ("completedtrips", false) =>
                query.OrderBy(driver => driver.CompletedTrips),
            ("name", true) => query.OrderByDescending(driver => driver.FullName),
            _ => query.OrderBy(driver => driver.FullName)
        };
}
