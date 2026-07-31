using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(activity => activity.Type).HasStringConversion();
        builder.Property(activity => activity.Message).HasMaxLength(300).IsRequired();
        builder.Property(activity => activity.EntityType).HasMaxLength(80).IsRequired();
        builder.Property(activity => activity.OccurredAtUtc)
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(activity => activity.OccurredAtUtc);
        builder.HasIndex(activity => new { activity.EntityType, activity.EntityId });
    }
}
