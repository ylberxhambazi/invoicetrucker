namespace FleetForge.Api.Domain.Entities;

public sealed class NewsletterSubscriber : EntityBase
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string NormalizedEmail { get; set; }
    public string? CompanyName { get; set; }
    public int? FleetSize { get; set; }
    public DateTime SubscribedAtUtc { get; set; }
}
