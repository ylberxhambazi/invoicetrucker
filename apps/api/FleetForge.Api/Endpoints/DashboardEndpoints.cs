using FleetForge.Api.Contracts.Dashboard;
using FleetForge.Api.Domain.Enums;
using FleetForge.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Endpoints;

public static class DashboardEndpoints
{
    public static IEndpointRouteBuilder MapDashboardEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/dashboard", GetDashboard)
            .WithTags("Dashboard")
            .WithName("GetDashboard")
            .Produces<DashboardResponse>();

        return endpoints;
    }

    private static async Task<IResult> GetDashboard(
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var nowUtc = DateTime.UtcNow;
        var today = DateOnly.FromDateTime(nowUtc);
        var monthStart = new DateOnly(today.Year, today.Month, 1);
        var nextMonth = monthStart.AddMonths(1);

        var activeTrucks = await dbContext.Trucks
            .AsNoTracking()
            .CountAsync(
                truck => truck.Status != TruckStatus.OutOfService,
                cancellationToken);
        var availableDrivers = await dbContext.Drivers
            .AsNoTracking()
            .CountAsync(
                driver => driver.Status == DriverStatus.Available,
                cancellationToken);
        var outstandingInvoices = await dbContext.Invoices
            .AsNoTracking()
            .Where(invoice =>
                invoice.Status == InvoiceStatus.Sent
                || invoice.Status == InvoiceStatus.Overdue)
            .SumAsync(invoice => (decimal?)invoice.TotalAmount, cancellationToken)
            ?? 0m;
        var monthlyRevenue = await dbContext.Invoices
            .AsNoTracking()
            .Where(invoice =>
                invoice.IssueDate >= monthStart
                && invoice.IssueDate < nextMonth
                && invoice.Status != InvoiceStatus.Draft)
            .SumAsync(invoice => (decimal?)invoice.TotalAmount, cancellationToken)
            ?? 0m;
        var monthlyExpenses = await dbContext.Expenses
            .AsNoTracking()
            .Where(expense => expense.Date >= monthStart && expense.Date < nextMonth)
            .SumAsync(expense => (decimal?)expense.Amount, cancellationToken)
            ?? 0m;
        var totalTrucks = await dbContext.Trucks
            .AsNoTracking()
            .CountAsync(cancellationToken);
        var utilizedTrucks = await dbContext.Trucks
            .AsNoTracking()
            .CountAsync(
                truck => truck.Status == TruckStatus.OnRoute,
                cancellationToken);
        var utilizationPercentage = totalTrucks == 0
            ? 0m
            : decimal.Round(utilizedTrucks * 100m / totalTrucks, 1);

        var invoiceStatusRecords = await dbContext.Invoices
            .AsNoTracking()
            .GroupBy(invoice => invoice.Status)
            .Select(group => new
            {
                Status = group.Key,
                Count = group.Count(),
                Amount = group.Sum(invoice => invoice.TotalAmount)
            })
            .ToListAsync(cancellationToken);
        var invoiceStatusSummary = invoiceStatusRecords
            .OrderBy(item => item.Status)
            .Select(item => new StatusValueResponse(
                item.Status.ToString(),
                item.Count,
                item.Amount))
            .ToList();

        var financialPerformance = await GetFinancialPerformance(
            dbContext,
            monthStart,
            cancellationToken);

        var recentActivity = await dbContext.ActivityLogs
            .AsNoTracking()
            .OrderByDescending(activity => activity.OccurredAtUtc)
            .ThenBy(activity => activity.Id)
            .Take(8)
            .Select(activity => new ActivityResponse(
                activity.Id,
                activity.Type.ToString(),
                activity.Message,
                activity.EntityType,
                activity.EntityId,
                activity.OccurredAtUtc))
            .ToListAsync(cancellationToken);

        var expirationLimit = today.AddDays(60);
        var upcomingRecords = await dbContext.Documents
            .AsNoTracking()
            .Where(document =>
                document.ExpirationDate.HasValue
                && document.ExpirationDate.Value >= today
                && document.ExpirationDate.Value <= expirationLimit)
            .OrderBy(document => document.ExpirationDate)
            .ThenBy(document => document.Id)
            .Take(8)
            .Select(document => new
            {
                document.Id,
                document.Type,
                document.Name,
                ExpirationDate = document.ExpirationDate!.Value,
                Owner = document.Truck != null
                    ? document.Truck.RegistrationNumber
                    : document.Driver != null
                        ? document.Driver.FullName
                        : document.Client != null
                            ? document.Client.CompanyName
                            : "Unassigned"
            })
            .ToListAsync(cancellationToken);

        var upcomingExpirations = upcomingRecords
            .Select(document => new DocumentExpirationResponse(
                document.Id,
                document.Type.ToString(),
                document.Name,
                document.Owner,
                document.ExpirationDate,
                document.ExpirationDate.DayNumber - today.DayNumber))
            .ToList();

        return Results.Ok(new DashboardResponse(
            activeTrucks,
            availableDrivers,
            outstandingInvoices,
            monthlyRevenue,
            monthlyExpenses,
            monthlyRevenue - monthlyExpenses,
            utilizationPercentage,
            invoiceStatusSummary,
            financialPerformance,
            recentActivity,
            upcomingExpirations));
    }

    private static async Task<IReadOnlyList<MonthlyFinancialResponse>>
        GetFinancialPerformance(
            FleetForgeDbContext dbContext,
            DateOnly currentMonth,
            CancellationToken cancellationToken)
    {
        var firstMonth = currentMonth.AddMonths(-11);
        var endMonth = currentMonth.AddMonths(1);

        var revenues = await dbContext.Invoices
            .AsNoTracking()
            .Where(invoice =>
                invoice.IssueDate >= firstMonth
                && invoice.IssueDate < endMonth
                && invoice.Status != InvoiceStatus.Draft)
            .GroupBy(invoice => new { invoice.IssueDate.Year, invoice.IssueDate.Month })
            .Select(group => new
            {
                group.Key.Year,
                group.Key.Month,
                Amount = group.Sum(invoice => invoice.TotalAmount)
            })
            .ToListAsync(cancellationToken);

        var expenses = await dbContext.Expenses
            .AsNoTracking()
            .Where(expense => expense.Date >= firstMonth && expense.Date < endMonth)
            .GroupBy(expense => new { expense.Date.Year, expense.Date.Month })
            .Select(group => new
            {
                group.Key.Year,
                group.Key.Month,
                Amount = group.Sum(expense => expense.Amount)
            })
            .ToListAsync(cancellationToken);

        return Enumerable.Range(0, 12)
            .Select(offset => firstMonth.AddMonths(offset))
            .Select(month =>
            {
                var revenue = revenues
                    .SingleOrDefault(item =>
                        item.Year == month.Year && item.Month == month.Month)
                    ?.Amount ?? 0m;
                var expense = expenses
                    .SingleOrDefault(item =>
                        item.Year == month.Year && item.Month == month.Month)
                    ?.Amount ?? 0m;

                return new MonthlyFinancialResponse(
                    month.Year,
                    month.Month,
                    month.ToString("MMM yyyy"),
                    revenue,
                    expense,
                    revenue - expense);
            })
            .ToList();
    }
}
