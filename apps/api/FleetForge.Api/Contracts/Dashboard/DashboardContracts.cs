namespace FleetForge.Api.Contracts.Dashboard;

public sealed record DashboardResponse(
    int ActiveTrucks,
    int AvailableDrivers,
    decimal OutstandingInvoices,
    decimal MonthlyRevenue,
    decimal MonthlyExpenses,
    decimal EstimatedProfit,
    decimal FleetUtilizationPercentage,
    IReadOnlyList<StatusValueResponse> InvoiceStatusSummary,
    IReadOnlyList<MonthlyFinancialResponse> FinancialPerformance,
    IReadOnlyList<ActivityResponse> RecentActivity,
    IReadOnlyList<DocumentExpirationResponse> UpcomingDocumentExpirations);

public sealed record StatusValueResponse(string Status, int Count, decimal Amount);

public sealed record MonthlyFinancialResponse(
    int Year,
    int Month,
    string Label,
    decimal Revenue,
    decimal Expenses,
    decimal Profit);

public sealed record ActivityResponse(
    Guid Id,
    string Type,
    string Message,
    string EntityType,
    Guid? EntityId,
    DateTime OccurredAtUtc);

public sealed record DocumentExpirationResponse(
    Guid Id,
    string Type,
    string Name,
    string Owner,
    DateOnly ExpirationDate,
    int DaysRemaining);
