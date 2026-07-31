using FleetForge.Api.Contracts.Common;

namespace FleetForge.Api.Contracts.Expenses;

public sealed class ExpenseQuery : ListQuery
{
    public string? Category { get; init; }
    public Guid? TruckId { get; init; }
    public DateOnly? DateFrom { get; init; }
    public DateOnly? DateTo { get; init; }
}

public sealed record ExpenseResponse(
    Guid Id,
    string Category,
    Guid? TruckId,
    string? Truck,
    DateOnly Date,
    string Supplier,
    string Description,
    decimal Amount,
    string Currency);
