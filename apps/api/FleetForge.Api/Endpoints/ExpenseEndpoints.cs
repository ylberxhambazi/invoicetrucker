using FleetForge.Api.Contracts.Common;
using FleetForge.Api.Contracts.Expenses;
using FleetForge.Api.Domain.Entities;
using FleetForge.Api.Domain.Enums;
using FleetForge.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Endpoints;

public static class ExpenseEndpoints
{
    private static readonly HashSet<string> SortFields =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "name",
            "date",
            "category",
            "supplier",
            "amount"
        };

    public static IEndpointRouteBuilder MapExpenseEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/expenses", GetExpenses)
            .WithTags("Expenses")
            .WithName("GetExpenses")
            .Produces<PaginatedResponse<ExpenseResponse>>()
            .ProducesValidationProblem();

        return endpoints;
    }

    private static async Task<IResult> GetExpenses(
        [AsParameters] ExpenseQuery request,
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var errors = QueryValidation.Validate(request, SortFields);
        EnumQuery.TryParse<ExpenseCategory>(
            request.Category,
            "category",
            errors,
            out var category);

        if (request.DateFrom.HasValue
            && request.DateTo.HasValue
            && request.DateFrom > request.DateTo)
        {
            errors["dateTo"] = ["Date to must be on or after date from."];
        }

        if (errors.Count > 0)
        {
            return EndpointResults.InvalidQuery(errors);
        }

        IQueryable<Expense> query = dbContext.Expenses.AsNoTracking();

        if (category.HasValue)
        {
            query = query.Where(expense => expense.Category == category.Value);
        }

        if (request.TruckId.HasValue)
        {
            query = query.Where(expense => expense.TruckId == request.TruckId.Value);
        }

        if (request.DateFrom.HasValue)
        {
            query = query.Where(expense => expense.Date >= request.DateFrom.Value);
        }

        if (request.DateTo.HasValue)
        {
            query = query.Where(expense => expense.Date <= request.DateTo.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            if (dbContext.Database.IsNpgsql())
            {
                var pattern = SearchPattern.Create(search);
                query = query.Where(expense =>
                    EF.Functions.ILike(
                        expense.Supplier,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || EF.Functions.ILike(
                        expense.Description,
                        pattern,
                        SearchPattern.EscapeCharacter)
                    || expense.Truck != null
                    && EF.Functions.ILike(
                        expense.Truck.RegistrationNumber,
                        pattern,
                        SearchPattern.EscapeCharacter));
            }
            else
            {
                var normalized = search.ToLowerInvariant();
                query = query.Where(expense =>
                    expense.Supplier.ToLower().Contains(normalized)
                    || expense.Description.ToLower().Contains(normalized)
                    || expense.Truck != null
                    && expense.Truck.RegistrationNumber.ToLower().Contains(normalized));
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
            .ThenBy(expense => expense.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(expense => new ExpenseResponse(
                expense.Id,
                expense.Category.ToString(),
                expense.TruckId,
                expense.Truck == null ? null : expense.Truck.RegistrationNumber,
                expense.Date,
                expense.Supplier,
                expense.Description,
                expense.Amount,
                expense.Currency))
            .ToListAsync(cancellationToken);

        return Results.Ok(
            PaginatedResponse<ExpenseResponse>.Create(
                items,
                page,
                pageSize,
                totalCount));
    }

    private static IOrderedQueryable<Expense> Order(
        IQueryable<Expense> query,
        string sortBy,
        bool descending) =>
        (sortBy.ToLowerInvariant(), descending) switch
        {
            ("category", true) =>
                query.OrderByDescending(expense => expense.Category),
            ("category", false) => query.OrderBy(expense => expense.Category),
            ("supplier", true) =>
                query.OrderByDescending(expense => expense.Supplier),
            ("supplier", false) => query.OrderBy(expense => expense.Supplier),
            ("amount", true) => query.OrderByDescending(expense => expense.Amount),
            ("amount", false) => query.OrderBy(expense => expense.Amount),
            ("date", false) => query.OrderBy(expense => expense.Date),
            ("name", true) or ("date", true) =>
                query.OrderByDescending(expense => expense.Date),
            _ => query.OrderBy(expense => expense.Date)
        };
}
