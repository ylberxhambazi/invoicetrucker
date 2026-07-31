using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal static class ConfigurationExtensions
{
    public static void ConfigureEntityBase<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : EntityBase
    {
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.CreatedAtUtc).HasColumnType("timestamp with time zone");
        builder.Property(entity => entity.UpdatedAtUtc).HasColumnType("timestamp with time zone");
    }

    public static PropertyBuilder<TEnum> HasStringConversion<TEnum>(
        this PropertyBuilder<TEnum> property,
        int maxLength = 32)
        where TEnum : struct, Enum
    {
        return property.HasConversion<string>().HasMaxLength(maxLength);
    }
}
