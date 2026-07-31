namespace FleetForge.Api.Domain.Entities;

public sealed class CompanyProfile : EntityBase
{
    public required string CompanyName { get; set; }
    public required string LegalName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string DefaultCurrency { get; set; }
    public required string TimeZone { get; set; }
    public required string VatNumber { get; set; }
    public required string InvoicePrefix { get; set; }
    public int PaymentTermsDays { get; set; }
}
