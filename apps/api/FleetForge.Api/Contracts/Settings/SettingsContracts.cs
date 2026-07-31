namespace FleetForge.Api.Contracts.Settings;

public sealed record SettingsResponse(
    string CompanyName,
    string LegalName,
    string Email,
    string Phone,
    string Address,
    string City,
    string Country,
    string DefaultCurrency,
    string TimeZone,
    string VatNumber,
    string InvoicePrefix,
    int PaymentTermsDays,
    DateTime UpdatedAtUtc);
