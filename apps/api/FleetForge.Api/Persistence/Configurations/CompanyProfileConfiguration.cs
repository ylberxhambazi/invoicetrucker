using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class CompanyProfileConfiguration
    : IEntityTypeConfiguration<CompanyProfile>
{
    public void Configure(EntityTypeBuilder<CompanyProfile> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(profile => profile.CompanyName).HasMaxLength(160).IsRequired();
        builder.Property(profile => profile.LegalName).HasMaxLength(200).IsRequired();
        builder.Property(profile => profile.Email).HasMaxLength(254).IsRequired();
        builder.Property(profile => profile.Phone).HasMaxLength(32).IsRequired();
        builder.Property(profile => profile.Address).HasMaxLength(200).IsRequired();
        builder.Property(profile => profile.City).HasMaxLength(100).IsRequired();
        builder.Property(profile => profile.Country).HasMaxLength(80).IsRequired();
        builder.Property(profile => profile.DefaultCurrency).HasMaxLength(3).IsFixedLength().IsRequired();
        builder.Property(profile => profile.TimeZone).HasMaxLength(80).IsRequired();
        builder.Property(profile => profile.VatNumber).HasMaxLength(40).IsRequired();
        builder.Property(profile => profile.InvoicePrefix).HasMaxLength(12).IsRequired();

        builder.HasIndex(profile => profile.CompanyName).IsUnique();
        builder.HasIndex(profile => profile.Email).IsUnique();
        builder.ToTable(table => table.HasCheckConstraint(
            "CK_CompanyProfiles_PaymentTermsDays",
            "\"PaymentTermsDays\" BETWEEN 1 AND 365"));
    }
}
