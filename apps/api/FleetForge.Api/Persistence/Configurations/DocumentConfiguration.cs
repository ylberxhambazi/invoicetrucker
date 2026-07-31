using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(document => document.Type).HasStringConversion();
        builder.Property(document => document.Name).HasMaxLength(180).IsRequired();
        builder.Property(document => document.ReferenceNumber).HasMaxLength(64).IsRequired();
        builder.Property(document => document.IssuedDate).HasColumnType("date");
        builder.Property(document => document.ExpirationDate).HasColumnType("date");

        builder.HasIndex(document => document.ReferenceNumber).IsUnique();
        builder.HasIndex(document => new { document.Type, document.ExpirationDate });
        builder.HasIndex(document => document.TruckId);
        builder.HasIndex(document => document.DriverId);
        builder.HasIndex(document => document.ClientId);

        builder.HasOne(document => document.Truck)
            .WithMany(truck => truck.Documents)
            .HasForeignKey(document => document.TruckId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(document => document.Driver)
            .WithMany(driver => driver.Documents)
            .HasForeignKey(document => document.DriverId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(document => document.Client)
            .WithMany(client => client.Documents)
            .HasForeignKey(document => document.ClientId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
