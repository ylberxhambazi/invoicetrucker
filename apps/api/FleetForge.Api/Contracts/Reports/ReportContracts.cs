namespace FleetForge.Api.Contracts.Reports;

public sealed record OverviewReportResponse(
    IReadOnlyList<MonthlyReportResponse> RevenueByMonth,
    IReadOnlyList<CategoryAmountResponse> ExpensesByCategory,
    IReadOnlyList<TruckProfitResponse> ProfitByTruck,
    IReadOnlyList<InvoiceStatusReportResponse> InvoicePaymentStatus,
    FleetUtilizationResponse FleetUtilization);

public sealed record MonthlyReportResponse(
    int Year,
    int Month,
    string Label,
    decimal Revenue,
    decimal Expenses,
    decimal Profit);

public sealed record CategoryAmountResponse(string Category, decimal Amount);

public sealed record TruckProfitResponse(
    Guid TruckId,
    string RegistrationNumber,
    decimal AllocatedRevenue,
    decimal Expenses,
    decimal EstimatedProfit);

public sealed record InvoiceStatusReportResponse(
    string Status,
    int Count,
    decimal Amount);

public sealed record FleetUtilizationResponse(
    int TotalTrucks,
    int UtilizedTrucks,
    decimal Percentage);
