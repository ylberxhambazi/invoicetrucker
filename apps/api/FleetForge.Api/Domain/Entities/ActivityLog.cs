using FleetForge.Api.Domain.Enums;

namespace FleetForge.Api.Domain.Entities;

public sealed class ActivityLog : EntityBase
{
    public ActivityType Type { get; set; }
    public required string Message { get; set; }
    public required string EntityType { get; set; }
    public Guid? EntityId { get; set; }
    public DateTime OccurredAtUtc { get; set; }
}
