using System.Net;
using System.Text.Json;
using FleetForge.Api.Domain.Entities;
using FleetForge.Api.Infrastructure;
using Microsoft.Extensions.Options;

namespace FleetForge.Api.Tests;

public sealed class NewsletterNotificationSenderTests
{
    [Fact]
    public async Task SendAsync_SendsNotificationToConfiguredAddress()
    {
        var handler = new CapturingHandler();
        var sender = CreateSender(handler, "test-api-key");
        var subscriber = CreateSubscriber();

        await sender.SendAsync(subscriber, CancellationToken.None);

        Assert.Equal("Bearer", handler.AuthorizationScheme);
        Assert.Equal("test-api-key", handler.AuthorizationParameter);
        using var body = JsonDocument.Parse(Assert.IsType<string>(handler.Body));
        Assert.Equal(
            "info@invoicetrucker.com",
            body.RootElement.GetProperty("to")[0].GetString());
        Assert.Equal(
            subscriber.Email,
            body.RootElement.GetProperty("reply_to").GetString());
        Assert.Contains(
            "Avery &lt;script&gt;",
            body.RootElement.GetProperty("html").GetString());
    }

    [Fact]
    public async Task SendAsync_DoesNothingWithoutApiKey()
    {
        var handler = new CapturingHandler();
        var sender = CreateSender(handler, null);

        await sender.SendAsync(CreateSubscriber(), CancellationToken.None);

        Assert.Equal(0, handler.RequestCount);
    }

    private static NewsletterNotificationSender CreateSender(
        CapturingHandler handler,
        string? apiKey)
    {
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.resend.com/")
        };
        var options = Options.Create(new NewsletterNotificationOptions
        {
            ResendApiKey = apiKey
        });

        return new NewsletterNotificationSender(httpClient, options);
    }

    private static NewsletterSubscriber CreateSubscriber() => new()
    {
        Id = Guid.NewGuid(),
        FullName = "Avery <script>",
        Email = "avery@example.com",
        NormalizedEmail = "avery@example.com",
        CompanyName = "Demo Transport",
        FleetSize = 8,
        SubscribedAtUtc = DateTime.UtcNow,
        CreatedAtUtc = DateTime.UtcNow,
        UpdatedAtUtc = DateTime.UtcNow
    };

    private sealed class CapturingHandler : HttpMessageHandler
    {
        public int RequestCount { get; private set; }
        public string? AuthorizationScheme { get; private set; }
        public string? AuthorizationParameter { get; private set; }
        public string? Body { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestCount++;
            AuthorizationScheme = request.Headers.Authorization?.Scheme;
            AuthorizationParameter = request.Headers.Authorization?.Parameter;
            Body = await request.Content!.ReadAsStringAsync(cancellationToken);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
