using FleetForge.Api.Contracts.Common;

namespace FleetForge.Api.Contracts.Documents;

public sealed class DocumentQuery : ListQuery
{
    public string? Type { get; init; }
    public string? Expiration { get; init; }
}

public sealed record DocumentResponse(
    Guid Id,
    string Type,
    string Name,
    string ReferenceNumber,
    DateOnly IssuedDate,
    DateOnly? ExpirationDate,
    string ExpirationStatus,
    DocumentOwnerResponse Owner);

public sealed record DocumentOwnerResponse(
    string Type,
    Guid Id,
    string Name);
