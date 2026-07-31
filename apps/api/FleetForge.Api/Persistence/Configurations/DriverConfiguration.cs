using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(driver => driver.FullName).HasMaxLength(120).IsRequired();
        builder.Property(driver => driver.Phone).HasMaxLength(32).IsRequired();
        builder.Property(driver => driver.Email).HasMaxLength(254).IsRequired();
        builder.Property(driver => driver.LicenceNumber).HasMaxLength(48).IsRequired();
        builder.Property(driver => driver.LicenceExpiration).HasColumnType("date");
        builder.Property(driver => driver.Status).HasStringConversion();

        builder.HasIndex(driver => driver.LicenceNumber).IsUnique();
        builder.HasIndex(driver => driver.Email).IsUnique();
        builder.HasIndex(driver => driver.Status);
        builder.HasIndex(driver => driver.LicenceExpiration);

        builder.ToTable(table =>
            table.HasCheckConstraint(
                "CK_Drivers_CompletedTrips",
                "\"CompletedTrips\" >= 0"));
    }
}
