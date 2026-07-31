namespace FleetForge.Api.Endpoints;

internal static class EndpointResults
{
    public static IResult InvalidQuery(Dictionary<string, string[]> errors) =>
        Results.ValidationProblem(
            errors,
            title: "One or more query parameters are invalid.",
            statusCode: StatusCodes.Status400BadRequest);

    public static IResult NotFound(string resource, Guid id) =>
        Results.Problem(
            title: $"{resource} not found",
            detail: $"No {resource.ToLowerInvariant()} exists with identifier '{id}'.",
            statusCode: StatusCodes.Status404NotFound);
}
