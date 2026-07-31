namespace FleetForge.Api.Contracts.Newsletter;

public sealed record NewsletterRequest(
    string FullName,
    string Email,
    string? CompanyName,
    int? FleetSize);

public sealed record NewsletterResponse(
    Guid Id,
    string FullName,
    string Email,
    DateTime SubscribedAtUtc);
