using FleetForge.Api.Contracts.Settings;
using FleetForge.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Endpoints;

public static class SettingsEndpoints
{
    public static IEndpointRouteBuilder MapSettingsEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/settings", GetSettings)
            .WithTags("Settings")
            .WithName("GetSettings")
            .Produces<SettingsResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> GetSettings(
        FleetForgeDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var settings = await dbContext.CompanyProfiles
            .AsNoTracking()
            .OrderBy(profile => profile.Id)
            .Select(profile => new SettingsResponse(
                profile.CompanyName,
                profile.LegalName,
                profile.Email,
                profile.Phone,
                profile.Address,
                profile.City,
                profile.Country,
                profile.DefaultCurrency,
                profile.TimeZone,
                profile.VatNumber,
                profile.InvoicePrefix,
                profile.PaymentTermsDays,
                profile.UpdatedAtUtc))
            .FirstOrDefaultAsync(cancellationToken);

        return settings is null
            ? Results.Problem(
                title: "Demo settings unavailable",
                detail: "The fictional company profile has not been configured.",
                statusCode: StatusCodes.Status404NotFound)
            : Results.Ok(settings);
    }
}
