using FleetForge.Api.Contracts.Common;

namespace FleetForge.Api.Contracts.Clients;

public sealed class ClientQuery : ListQuery
{
    public bool? IsActive { get; init; }
    public string? Country { get; init; }
}

public sealed record ClientSummaryResponse(
    Guid Id,
    string CompanyName,
    string ContactPerson,
    string Country,
    string Email,
    string Phone,
    int ActiveInvoices,
    decimal TotalBilled,
    bool IsActive);

public sealed record ClientDetailResponse(
    Guid Id,
    string CompanyName,
    string ContactPerson,
    string Country,
    string Email,
    string Phone,
    bool IsActive,
    int ActiveInvoices,
    decimal TotalBilled,
    IReadOnlyList<ClientInvoiceResponse> RecentInvoices,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);

public sealed record ClientInvoiceResponse(
    Guid Id,
    string InvoiceNumber,
    DateOnly IssueDate,
    DateOnly DueDate,
    decimal Amount,
    string Currency,
    string Status);
