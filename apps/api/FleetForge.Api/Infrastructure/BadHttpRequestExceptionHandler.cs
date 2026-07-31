using Microsoft.AspNetCore.Diagnostics;

namespace FleetForge.Api.Infrastructure;

public sealed class BadHttpRequestExceptionHandler(
    ILogger<BadHttpRequestExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not BadHttpRequestException badRequest)
        {
            return false;
        }

        logger.LogInformation(
            "Request binding failed with status code {StatusCode}",
            badRequest.StatusCode);

        await Results.Problem(
                title: "Invalid request",
                detail: "One or more request values could not be parsed.",
                statusCode: badRequest.StatusCode)
            .ExecuteAsync(httpContext);

        return true;
    }
}
