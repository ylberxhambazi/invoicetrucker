using FleetForge.Api.Contracts.Reports;
using FleetForge.Api.Domain.Enums;
using FleetForge.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Endpoints;

public static class ReportEndpoints
{
    public static IEndpointRouteBuilder MapReportEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/reports/overview", GetOverview)
            .WithTags("Reports")
            .WithName("GetReportsOverview")
            .Produces<OverviewReportResponse>();

        return endpoints;
    }

    private static async Task<IResult> GetOverview(
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var currentMonth = new DateOnly(today.Year, today.Month, 1);
        var firstMonth = currentMonth.AddMonths(-11);
        var endMonth = currentMonth.AddMonths(1);

        var revenueRecords = await dbContext.Invoices
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

        var expenseRecords = await dbContext.Expenses
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

        var revenueByMonth = Enumerable.Range(0, 12)
            .Select(offset => firstMonth.AddMonths(offset))
            .Select(month =>
            {
                var revenue = revenueRecords
                    .SingleOrDefault(item =>
                        item.Year == month.Year && item.Month == month.Month)
                    ?.Amount ?? 0m;
                var expenses = expenseRecords
                    .SingleOrDefault(item =>
                        item.Year == month.Year && item.Month == month.Month)
                    ?.Amount ?? 0m;

                return new MonthlyReportResponse(
                    month.Year,
                    month.Month,
                    month.ToString("MMM yyyy"),
                    revenue,
                    expenses,
                    revenue - expenses);
            })
            .ToList();

        var expenseCategoryRecords = await dbContext.Expenses
            .AsNoTracking()
            .GroupBy(expense => expense.Category)
            .Select(group => new
            {
                Category = group.Key,
                Amount = group.Sum(expense => expense.Amount)
            })
            .ToListAsync(cancellationToken);
        var expensesByCategory = expenseCategoryRecords
            .OrderByDescending(item => item.Amount)
            .ThenBy(item => item.Category)
            .Select(item => new CategoryAmountResponse(
                item.Category.ToString(),
                item.Amount))
            .ToList();

        var profitByTruck = await dbContext.Trucks
            .AsNoTracking()
            .OrderBy(truck => truck.RegistrationNumber)
            .Select(truck => new TruckProfitResponse(
                truck.Id,
                truck.RegistrationNumber,
                truck.Invoices
                    .Where(invoice => invoice.Status != InvoiceStatus.Draft)
                    .Sum(invoice => (decimal?)invoice.TotalAmount) ?? 0m,
                truck.Expenses.Sum(expense => (decimal?)expense.Amount) ?? 0m,
                (truck.Invoices
                    .Where(invoice => invoice.Status != InvoiceStatus.Draft)
                    .Sum(invoice => (decimal?)invoice.TotalAmount) ?? 0m)
                - (truck.Expenses.Sum(expense => (decimal?)expense.Amount) ?? 0m)))
            .ToListAsync(cancellationToken);

        var invoicePaymentRecords = await dbContext.Invoices
            .AsNoTracking()
            .GroupBy(invoice => invoice.Status)
            .Select(group => new
            {
                Status = group.Key,
                Count = group.Count(),
                Amount = group.Sum(invoice => invoice.TotalAmount)
            })
            .ToListAsync(cancellationToken);
        var invoicePaymentStatus = invoicePaymentRecords
            .OrderBy(item => item.Status)
            .Select(item => new InvoiceStatusReportResponse(
                item.Status.ToString(),
                item.Count,
                item.Amount))
            .ToList();

        var totalTrucks = await dbContext.Trucks
            .AsNoTracking()
            .CountAsync(cancellationToken);
        var utilizedTrucks = await dbContext.Trucks
            .AsNoTracking()
            .CountAsync(
                truck => truck.Status == TruckStatus.OnRoute,
                cancellationToken);
        var fleetUtilization = new FleetUtilizationResponse(
            totalTrucks,
            utilizedTrucks,
            totalTrucks == 0
                ? 0m
                : decimal.Round(utilizedTrucks * 100m / totalTrucks, 1));

        return Results.Ok(new OverviewReportResponse(
            revenueByMonth,
            expensesByCategory,
            profitByTruck,
            invoicePaymentStatus,
            fleetUtilization));
    }
}
