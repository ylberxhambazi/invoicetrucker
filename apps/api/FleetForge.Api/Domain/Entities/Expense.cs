using FleetForge.Api.Domain.Enums;

namespace FleetForge.Api.Domain.Entities;

public sealed class Expense : EntityBase
{
    public ExpenseCategory Category { get; set; }
    public Guid? TruckId { get; set; }
    public Truck? Truck { get; set; }
    public DateOnly Date { get; set; }
    public required string Supplier { get; set; }
    public required string Description { get; set; }
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
}
