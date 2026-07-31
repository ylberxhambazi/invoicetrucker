using FleetForge.Api.Contracts.Common;

namespace FleetForge.Api.Contracts.Trucks;

public sealed class TruckQuery : ListQuery
{
    public string? Status { get; init; }
    public int? Year { get; init; }
}

public sealed record TruckSummaryResponse(
    Guid Id,
    string RegistrationNumber,
    string Make,
    string Model,
    int Year,
    string Status,
    string? AssignedDriver,
    long CurrentMileage,
    DateOnly InsuranceExpiration,
    DateOnly TechnicalInspectionExpiration);

public sealed record TruckDetailResponse(
    Guid Id,
    string RegistrationNumber,
    string Make,
    string Model,
    int Year,
    string Status,
    long CurrentMileage,
    DateOnly InsuranceExpiration,
    DateOnly TechnicalInspectionExpiration,
    AssignedDriverResponse? AssignedDriver,
    decimal TotalExpenses,
    int DocumentCount,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);

public sealed record AssignedDriverResponse(
    Guid Id,
    string FullName,
    string Phone,
    string Status);
