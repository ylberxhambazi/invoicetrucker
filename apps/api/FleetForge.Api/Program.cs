using System.Threading.RateLimiting;
using FleetForge.Api.Endpoints;
using FleetForge.Api.Infrastructure;
using FleetForge.Api.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console());

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<BadHttpRequestExceptionHandler>();
builder.Services.Configure<RouteHandlerOptions>(options =>
    options.ThrowOnBadRequest = true);
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var connectionString = builder.Configuration.GetConnectionString("FleetForge")
    ?? throw new InvalidOperationException(
        "Connection string 'FleetForge' is not configured.");

builder.Services.AddDbContext<FleetForgeDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddHealthChecks()
    .AddDbContextCheck<FleetForgeDbContext>(
        "postgresql",
        HealthStatus.Unhealthy,
        ["database"]);

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        if (allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, cancellationToken) =>
    {
        if (context.Lease.TryGetMetadata(
                MetadataName.RetryAfter,
                out var retryAfter))
        {
            context.HttpContext.Response.Headers.RetryAfter =
                ((int)Math.Ceiling(retryAfter.TotalSeconds)).ToString();
        }

        await Results.Problem(
                title: "Too many requests",
                detail: "Please wait before submitting another early-access request.",
                statusCode: StatusCodes.Status429TooManyRequests)
            .ExecuteAsync(context.HttpContext);
    };

    options.AddPolicy("newsletter", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString()
                ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            }));
});

var app = builder.Build();

if (app.Configuration.GetValue<bool>("Database:ApplyMigrations"))
{
    await using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FleetForgeDbContext>();
    app.Logger.LogInformation(
        "Applying pending InvoiceTrucker database migrations");
    await dbContext.Database.MigrateAsync();
}

app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseCors();
app.UseRateLimiter();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            status = report.Status.ToString()
        });
    }
});
app.MapFleetForgeApi();

app.Run();

public partial class Program;
