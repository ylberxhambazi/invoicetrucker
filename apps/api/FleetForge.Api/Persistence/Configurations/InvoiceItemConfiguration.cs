using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(item => item.Description).HasMaxLength(240).IsRequired();
        builder.Property(item => item.Quantity).HasPrecision(10, 2);
        builder.Property(item => item.UnitPrice).HasPrecision(18, 2);
        builder.Property(item => item.LineTotal).HasPrecision(18, 2);

        builder.HasIndex(item => item.InvoiceId);
        builder.HasOne(item => item.Invoice)
            .WithMany(invoice => invoice.Items)
            .HasForeignKey(item => item.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(table =>
        {
            table.HasCheckConstraint("CK_InvoiceItems_Quantity", "\"Quantity\" > 0");
            table.HasCheckConstraint("CK_InvoiceItems_UnitPrice", "\"UnitPrice\" >= 0");
            table.HasCheckConstraint("CK_InvoiceItems_LineTotal", "\"LineTotal\" >= 0");
        });
    }
}
