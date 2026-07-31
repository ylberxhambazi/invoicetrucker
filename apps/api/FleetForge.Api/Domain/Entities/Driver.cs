using FleetForge.Api.Domain.Enums;

namespace FleetForge.Api.Domain.Entities;

public sealed class Driver : EntityBase
{
    public required string FullName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string LicenceNumber { get; set; }
    public DateOnly LicenceExpiration { get; set; }
    public DriverStatus Status { get; set; }
    public int CompletedTrips { get; set; }
    public Truck? AssignedTruck { get; set; }
    public ICollection<Document> Documents { get; set; } = [];
}
