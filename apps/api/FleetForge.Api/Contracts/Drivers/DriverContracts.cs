using FleetForge.Api.Contracts.Common;

namespace FleetForge.Api.Contracts.Drivers;

public sealed class DriverQuery : ListQuery
{
    public string? Status { get; init; }
}

public sealed record DriverSummaryResponse(
    Guid Id,
    string FullName,
    string Phone,
    string LicenceNumber,
    DateOnly LicenceExpiration,
    string? AssignedTruck,
    string Status,
    int CompletedTrips);

public sealed record DriverDetailResponse(
    Guid Id,
    string FullName,
    string Phone,
    string Email,
    string LicenceNumber,
    DateOnly LicenceExpiration,
    string Status,
    int CompletedTrips,
    AssignedTruckResponse? AssignedTruck,
    int DocumentCount,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);

public sealed record AssignedTruckResponse(
    Guid Id,
    string RegistrationNumber,
    string Make,
    string Model,
    string Status);
