using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FleetForge.Api.Contracts.Common;
using FleetForge.Api.Contracts.Dashboard;
using FleetForge.Api.Contracts.Invoices;
using FleetForge.Api.Contracts.Newsletter;
using FleetForge.Api.Contracts.Settings;
using FleetForge.Api.Contracts.Trucks;
using FleetForge.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FleetForge.Api.Tests;

public sealed class ApiIntegrationTests : IClassFixture<FleetForgeApiFactory>
{
    private readonly HttpClient _client;
    private readonly FleetForgeApiFactory _factory;

    public ApiIntegrationTests(FleetForgeApiFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_ReturnsHealthyStatus()
    {
        var response = await _client.GetAsync("/health");
        var body = await response.Content.ReadFromJsonAsync<HealthResponse>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Healthy", body?.Status);
    }

    [Fact]
    public async Task NewsletterRegistration_ReturnsCreated()
    {
        var request = new NewsletterRequest(
            "Avery Demo",
            "avery.success@invoicetrucker.example",
            "Demo Transport Studio",
            8);

        var response = await _client.PostAsJsonAsync("/api/newsletter", request);
        var body = await response.Content.ReadFromJsonAsync<NewsletterResponse>();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(request.Email, body?.Email);
        Assert.Equal(request.FullName, body?.FullName);
        Assert.NotEqual(Guid.Empty, body?.Id);
        Assert.Equal(DateTimeKind.Utc, body?.SubscribedAtUtc.Kind);
    }

    [Fact]
    public async Task NewsletterRegistration_ReturnsConflictForNormalizedDuplicate()
    {
        var initial = new NewsletterRequest(
            "Casey Demo",
            "casey.duplicate@invoicetrucker.example",
            null,
            null);
        var duplicate = initial with
        {
            Email = "  CASEY.DUPLICATE@INVOICETRUCKER.EXAMPLE "
        };

        var firstResponse = await _client.PostAsJsonAsync("/api/newsletter", initial);
        var duplicateResponse = await _client.PostAsJsonAsync(
            "/api/newsletter",
            duplicate);
        using var problem = await JsonDocument.ParseAsync(
            await duplicateResponse.Content.ReadAsStreamAsync());

        Assert.Equal(HttpStatusCode.Created, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, duplicateResponse.StatusCode);
        Assert.Equal(
            "application/problem+json",
            duplicateResponse.Content.Headers.ContentType?.MediaType);
        Assert.Equal(
            "Email already registered",
            problem.RootElement.GetProperty("title").GetString());
    }

    [Fact]
    public async Task NewsletterRegistration_ReturnsValidationProblem()
    {
        var request = new NewsletterRequest("", "not-an-email", null, 0);

        var response = await _client.PostAsJsonAsync("/api/newsletter", request);
        using var problem = await JsonDocument.ParseAsync(
            await response.Content.ReadAsStreamAsync());

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(
            "application/problem+json",
            response.Content.Headers.ContentType?.MediaType);
        Assert.True(problem.RootElement.GetProperty("errors").TryGetProperty(
            "FullName",
            out _));
        Assert.True(problem.RootElement.GetProperty("errors").TryGetProperty(
            "Email",
            out _));
    }

    [Fact]
    public async Task Trucks_ReturnsStablePaginatedResults()
    {
        var response = await _client.GetAsync(
            "/api/trucks?page=2&pageSize=5&sortBy=registrationNumber&sortDirection=asc");
        var body = await response.Content.ReadFromJsonAsync<
            PaginatedResponse<TruckSummaryResponse>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(body);
        Assert.Equal(2, body.Page);
        Assert.Equal(5, body.PageSize);
        Assert.Equal(15, body.TotalCount);
        Assert.Equal(3, body.TotalPages);
        Assert.Equal(5, body.Items.Count);
        Assert.Equal("IT-DEMO-006", body.Items[0].RegistrationNumber);
    }

    [Fact]
    public async Task Trucks_SearchIsCaseInsensitive()
    {
        var response = await _client.GetAsync(
            "/api/trucks?search=VOLVO&pageSize=20&sortBy=registrationNumber");
        var body = await response.Content.ReadFromJsonAsync<
            PaginatedResponse<TruckSummaryResponse>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(body);
        Assert.Equal(3, body.TotalCount);
        Assert.All(body.Items, truck =>
            Assert.Equal("Volvo", truck.Make));
    }

    [Fact]
    public async Task Dashboard_ReturnsSeededOperationalSummary()
    {
        var response = await _client.GetAsync("/api/dashboard");
        var body = await response.Content.ReadFromJsonAsync<DashboardResponse>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(body);
        Assert.Equal(12, body.ActiveTrucks);
        Assert.Equal(3, body.AvailableDrivers);
        Assert.Equal(12, body.FinancialPerformance.Count);
        Assert.NotEmpty(body.InvoiceStatusSummary);
        Assert.NotEmpty(body.RecentActivity);
    }

    [Fact]
    public async Task InvoiceDetail_ReturnsClientTruckAndItems()
    {
        const string invoiceId = "00000000-0000-0004-0000-000000000001";

        var response = await _client.GetAsync($"/api/invoices/{invoiceId}");
        var body = await response.Content.ReadFromJsonAsync<InvoiceDetailResponse>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(body);
        Assert.Equal("IT-2001", body.InvoiceNumber);
        Assert.Equal("Northlane Foods", body.Client.CompanyName);
        Assert.NotNull(body.Truck);
        Assert.Equal(2, body.Items.Count);
        Assert.Equal(
            body.Amount,
            body.Items.Sum(item => item.LineTotal));
    }

    [Fact]
    public async Task InvalidPagination_ReturnsProblemDetails()
    {
        var response = await _client.GetAsync("/api/trucks?page=0&pageSize=101");
        using var problem = await JsonDocument.ParseAsync(
            await response.Content.ReadAsStreamAsync());

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(
            "application/problem+json",
            response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(
            400,
            problem.RootElement.GetProperty("status").GetInt32());
        Assert.True(problem.RootElement.GetProperty("errors").TryGetProperty(
            "pageSize",
            out _));
    }

    [Fact]
    public async Task MalformedQueryValue_ReturnsProblemDetails()
    {
        var response = await _client.GetAsync("/api/trucks?page=not-a-number");
        using var problem = await JsonDocument.ParseAsync(
            await response.Content.ReadAsStreamAsync());

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(
            "application/problem+json",
            response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(
            "Invalid request",
            problem.RootElement.GetProperty("title").GetString());
    }

    [Fact]
    public async Task SeedData_HasExpectedDeterministicRecordCounts()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FleetForgeDbContext>();

        Assert.Equal(15, await dbContext.Trucks.CountAsync());
        Assert.Equal(12, await dbContext.Drivers.CountAsync());
        Assert.Equal(20, await dbContext.Clients.CountAsync());
        Assert.Equal(50, await dbContext.Invoices.CountAsync());
        Assert.Equal(80, await dbContext.Expenses.CountAsync());
        Assert.Equal(25, await dbContext.Documents.CountAsync());
        Assert.Equal(30, await dbContext.ActivityLogs.CountAsync());
        Assert.Single(await dbContext.CompanyProfiles.ToListAsync());

        var invoiceMonths = (await dbContext.Invoices
            .Select(invoice => invoice.IssueDate)
            .ToListAsync())
            .Select(date => (date.Year, date.Month))
            .Distinct()
            .Count();
        Assert.Equal(12, invoiceMonths);
    }

    [Fact]
    public async Task Settings_ReturnsReadOnlyFictionalCompanyProfile()
    {
        var response = await _client.GetAsync("/api/settings");
        var body = await response.Content.ReadFromJsonAsync<SettingsResponse>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(body);
        Assert.Equal("Northstar Demo Logistics", body.CompanyName);
        Assert.Equal("EUR", body.DefaultCurrency);
        Assert.Equal(30, body.PaymentTermsDays);
    }

    private sealed record HealthResponse(string Status);
}
