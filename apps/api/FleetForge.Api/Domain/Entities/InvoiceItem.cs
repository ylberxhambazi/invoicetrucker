namespace FleetForge.Api.Domain.Entities;

public sealed class InvoiceItem : EntityBase
{
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
    public required string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}
