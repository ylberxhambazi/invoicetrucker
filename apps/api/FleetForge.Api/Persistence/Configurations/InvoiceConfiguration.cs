using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(invoice => invoice.InvoiceNumber).HasMaxLength(32).IsRequired();
        builder.Property(invoice => invoice.IssueDate).HasColumnType("date");
        builder.Property(invoice => invoice.DueDate).HasColumnType("date");
        builder.Property(invoice => invoice.TotalAmount).HasPrecision(18, 2);
        builder.Property(invoice => invoice.Currency).HasMaxLength(3).IsFixedLength().IsRequired();
        builder.Property(invoice => invoice.Status).HasStringConversion();
        builder.Property(invoice => invoice.PaidAtUtc).HasColumnType("timestamp with time zone");
        builder.Property(invoice => invoice.Notes).HasMaxLength(500).IsRequired();

        builder.HasIndex(invoice => invoice.InvoiceNumber).IsUnique();
        builder.HasIndex(invoice => invoice.IssueDate);
        builder.HasIndex(invoice => new { invoice.ClientId, invoice.Status });
        builder.HasIndex(invoice => new { invoice.TruckId, invoice.IssueDate });
        builder.HasIndex(invoice => new { invoice.Status, invoice.DueDate });

        builder.HasOne(invoice => invoice.Client)
            .WithMany(client => client.Invoices)
            .HasForeignKey(invoice => invoice.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(invoice => invoice.Truck)
            .WithMany(truck => truck.Invoices)
            .HasForeignKey(invoice => invoice.TruckId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable(table =>
        {
            table.HasCheckConstraint("CK_Invoices_TotalAmount", "\"TotalAmount\" >= 0");
            table.HasCheckConstraint("CK_Invoices_DueDate", "\"DueDate\" >= \"IssueDate\"");
        });
    }
}
