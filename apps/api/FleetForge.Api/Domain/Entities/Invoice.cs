using FleetForge.Api.Domain.Enums;

namespace FleetForge.Api.Domain.Entities;

public sealed class Invoice : EntityBase
{
    public required string InvoiceNumber { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public Guid? TruckId { get; set; }
    public Truck? Truck { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public required string Currency { get; set; }
    public InvoiceStatus Status { get; set; }
    public DateTime? PaidAtUtc { get; set; }
    public required string Notes { get; set; }
    public ICollection<InvoiceItem> Items { get; set; } = [];
}
