using FleetForge.Api.Contracts.Newsletter;
using FleetForge.Api.Domain.Entities;
using FleetForge.Api.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FleetForge.Api.Endpoints;

public static class NewsletterEndpoints
{
    public static IEndpointRouteBuilder MapNewsletterEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/newsletter", Register)
            .WithTags("Newsletter")
            .WithName("RegisterNewsletterSubscriber")
            .RequireRateLimiting("newsletter")
            .Produces<NewsletterResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status429TooManyRequests);

        return endpoints;
    }

    private static async Task<IResult> Register(
        NewsletterRequest request,
        IValidator<NewsletterRequest> validator,
        FleetForgeDbContext dbContext,
        ILogger<Program> logger,
        CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
        {
            return Results.ValidationProblem(
                validation.ToDictionary(),
                title: "One or more registration fields are invalid.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var alreadyRegistered = await dbContext.NewsletterSubscribers
            .AsNoTracking()
            .AnyAsync(
                subscriber => subscriber.NormalizedEmail == normalizedEmail,
                cancellationToken);

        if (alreadyRegistered)
        {
            return DuplicateEmailProblem();
        }

        var nowUtc = DateTime.UtcNow;
        var subscriber = new NewsletterSubscriber
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            NormalizedEmail = normalizedEmail,
            CompanyName = string.IsNullOrWhiteSpace(request.CompanyName)
                ? null
                : request.CompanyName.Trim(),
            FleetSize = request.FleetSize,
            SubscribedAtUtc = nowUtc,
            CreatedAtUtc = nowUtc,
            UpdatedAtUtc = nowUtc
        };

        dbContext.NewsletterSubscribers.Add(subscriber);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception)
            when (IsNormalizedEmailViolation(exception))
        {
            logger.LogInformation(
                "Newsletter registration rejected for an existing normalized email");
            return DuplicateEmailProblem();
        }

        logger.LogInformation(
            "Newsletter subscriber {SubscriberId} registered",
            subscriber.Id);

        return Results.Json(
            new NewsletterResponse(
                subscriber.Id,
                subscriber.FullName,
                subscriber.Email,
                subscriber.SubscribedAtUtc),
            statusCode: StatusCodes.Status201Created);
    }

    private static IResult DuplicateEmailProblem() =>
        Results.Problem(
            title: "Email already registered",
            detail: "This email address is already on the early-access list.",
            statusCode: StatusCodes.Status409Conflict);

    private static bool IsNormalizedEmailViolation(DbUpdateException exception) =>
        exception.InnerException is PostgresException
        {
            SqlState: PostgresErrorCodes.UniqueViolation,
            ConstraintName: "IX_NewsletterSubscribers_NormalizedEmail"
        };
}
