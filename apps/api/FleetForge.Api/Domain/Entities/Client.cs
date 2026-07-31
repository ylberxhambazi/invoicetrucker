namespace FleetForge.Api.Domain.Entities;

public sealed class Client : EntityBase
{
    public required string CompanyName { get; set; }
    public required string ContactPerson { get; set; }
    public required string Country { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Invoice> Invoices { get; set; } = [];
    public ICollection<Document> Documents { get; set; } = [];
}
