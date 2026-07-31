using FleetForge.Api.Contracts.Newsletter;
using FluentValidation;

namespace FleetForge.Api.Validation;

public sealed class NewsletterRequestValidator : AbstractValidator<NewsletterRequest>
{
    public NewsletterRequestValidator()
    {
        RuleFor(request => request.FullName)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(request => request.Email)
            .NotEmpty()
            .MaximumLength(254)
            .EmailAddress();

        RuleFor(request => request.CompanyName)
            .MaximumLength(160)
            .When(request => !string.IsNullOrWhiteSpace(request.CompanyName));

        RuleFor(request => request.FleetSize)
            .InclusiveBetween(1, 100_000)
            .When(request => request.FleetSize.HasValue);
    }
}
