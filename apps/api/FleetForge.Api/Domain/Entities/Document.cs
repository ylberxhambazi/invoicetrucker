using FleetForge.Api.Domain.Enums;

namespace FleetForge.Api.Domain.Entities;

public sealed class Document : EntityBase
{
    public DocumentType Type { get; set; }
    public required string Name { get; set; }
    public required string ReferenceNumber { get; set; }
    public DateOnly IssuedDate { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public Guid? TruckId { get; set; }
    public Truck? Truck { get; set; }
    public Guid? DriverId { get; set; }
    public Driver? Driver { get; set; }
    public Guid? ClientId { get; set; }
    public Client? Client { get; set; }
}
