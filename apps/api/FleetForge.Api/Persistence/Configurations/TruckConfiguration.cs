using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class TruckConfiguration : IEntityTypeConfiguration<Truck>
{
    public void Configure(EntityTypeBuilder<Truck> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(truck => truck.RegistrationNumber).HasMaxLength(32).IsRequired();
        builder.Property(truck => truck.Make).HasMaxLength(64).IsRequired();
        builder.Property(truck => truck.Model).HasMaxLength(64).IsRequired();
        builder.Property(truck => truck.Status).HasStringConversion();
        builder.Property(truck => truck.InsuranceExpiration).HasColumnType("date");
        builder.Property(truck => truck.TechnicalInspectionExpiration).HasColumnType("date");

        builder.HasIndex(truck => truck.RegistrationNumber).IsUnique();
        builder.HasIndex(truck => truck.Status);
        builder.HasIndex(truck => truck.InsuranceExpiration);
        builder.HasIndex(truck => truck.TechnicalInspectionExpiration);
        builder.HasIndex(truck => truck.AssignedDriverId)
            .IsUnique()
            .HasFilter("\"AssignedDriverId\" IS NOT NULL");

        builder.HasOne(truck => truck.AssignedDriver)
            .WithOne(driver => driver.AssignedTruck)
            .HasForeignKey<Truck>(truck => truck.AssignedDriverId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable(table =>
        {
            table.HasCheckConstraint("CK_Trucks_Year", "\"Year\" BETWEEN 1990 AND 2100");
            table.HasCheckConstraint("CK_Trucks_CurrentMileage", "\"CurrentMileage\" >= 0");
        });
    }
}
