using FleetForge.Api.Domain.Enums;

namespace FleetForge.Api.Domain.Entities;

public sealed class Truck : EntityBase
{
    public required string RegistrationNumber { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
    public int Year { get; set; }
    public TruckStatus Status { get; set; }
    public long CurrentMileage { get; set; }
    public DateOnly InsuranceExpiration { get; set; }
    public DateOnly TechnicalInspectionExpiration { get; set; }
    public Guid? AssignedDriverId { get; set; }
    public Driver? AssignedDriver { get; set; }
    public ICollection<Invoice> Invoices { get; set; } = [];
    public ICollection<Expense> Expenses { get; set; } = [];
    public ICollection<Document> Documents { get; set; } = [];
}
