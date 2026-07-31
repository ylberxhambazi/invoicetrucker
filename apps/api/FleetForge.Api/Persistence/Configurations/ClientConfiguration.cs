using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(client => client.CompanyName).HasMaxLength(160).IsRequired();
        builder.Property(client => client.ContactPerson).HasMaxLength(120).IsRequired();
        builder.Property(client => client.Country).HasMaxLength(80).IsRequired();
        builder.Property(client => client.Email).HasMaxLength(254).IsRequired();
        builder.Property(client => client.Phone).HasMaxLength(32).IsRequired();

        builder.HasIndex(client => client.CompanyName);
        builder.HasIndex(client => client.Email).IsUnique();
        builder.HasIndex(client => new { client.IsActive, client.CompanyName });
    }
}
