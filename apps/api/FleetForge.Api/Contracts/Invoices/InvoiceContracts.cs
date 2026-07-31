using FleetForge.Api.Contracts.Common;

namespace FleetForge.Api.Contracts.Invoices;

public sealed class InvoiceQuery : ListQuery
{
    public string? Status { get; init; }
    public Guid? ClientId { get; init; }
    public DateOnly? IssueDateFrom { get; init; }
    public DateOnly? IssueDateTo { get; init; }
}

public sealed record InvoiceSummaryResponse(
    Guid Id,
    string InvoiceNumber,
    Guid ClientId,
    string Client,
    string? Truck,
    DateOnly IssueDate,
    DateOnly DueDate,
    decimal Amount,
    string Currency,
    string Status);

public sealed record InvoiceDetailResponse(
    Guid Id,
    string InvoiceNumber,
    InvoiceClientResponse Client,
    InvoiceTruckResponse? Truck,
    DateOnly IssueDate,
    DateOnly DueDate,
    decimal Amount,
    string Currency,
    string Status,
    DateTime? PaidAtUtc,
    string Notes,
    IReadOnlyList<InvoiceItemResponse> Items,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);

public sealed record InvoiceClientResponse(
    Guid Id,
    string CompanyName,
    string ContactPerson,
    string Email,
    string Country);

public sealed record InvoiceTruckResponse(
    Guid Id,
    string RegistrationNumber,
    string Make,
    string Model);

public sealed record InvoiceItemResponse(
    Guid Id,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineTotal);
