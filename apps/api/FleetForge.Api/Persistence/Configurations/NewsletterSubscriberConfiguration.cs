using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class NewsletterSubscriberConfiguration
    : IEntityTypeConfiguration<NewsletterSubscriber>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscriber> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(subscriber => subscriber.FullName).HasMaxLength(120).IsRequired();
        builder.Property(subscriber => subscriber.Email).HasMaxLength(254).IsRequired();
        builder.Property(subscriber => subscriber.NormalizedEmail).HasMaxLength(254).IsRequired();
        builder.Property(subscriber => subscriber.CompanyName).HasMaxLength(160);
        builder.Property(subscriber => subscriber.SubscribedAtUtc)
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(subscriber => subscriber.NormalizedEmail).IsUnique();
        builder.HasIndex(subscriber => subscriber.SubscribedAtUtc);

        builder.ToTable(table =>
            table.HasCheckConstraint(
                "CK_NewsletterSubscribers_FleetSize",
                "\"FleetSize\" IS NULL OR \"FleetSize\" > 0"));
    }
}
