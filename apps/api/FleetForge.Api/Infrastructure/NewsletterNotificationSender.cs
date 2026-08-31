using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using FleetForge.Api.Domain.Entities;
using Microsoft.Extensions.Options;

namespace FleetForge.Api.Infrastructure;

public sealed class NewsletterNotificationOptions
{
    public string? ResendApiKey { get; init; }
    public string RecipientAddress { get; init; } = "info@invoicetrucker.com";
    public string FromAddress { get; init; } =
        "InvoiceTrucker Early Access <early-access@invoicetrucker.com>";
}

public sealed class NewsletterNotificationSender(
    HttpClient httpClient,
    IOptions<NewsletterNotificationOptions> options)
{
    public async Task SendAsync(
        NewsletterSubscriber subscriber,
        CancellationToken cancellationToken)
    {
        var settings = options.Value;
        if (string.IsNullOrWhiteSpace(settings.ResendApiKey))
        {
            return;
        }

        static string Encode(string value) => HtmlEncoder.Default.Encode(value);
        var companyName = string.IsNullOrWhiteSpace(subscriber.CompanyName)
            ? "Not provided"
            : Encode(subscriber.CompanyName);
        var fleetSize = subscriber.FleetSize?.ToString() ?? "Not provided";

        using var request = new HttpRequestMessage(HttpMethod.Post, "emails")
        {
            Content = JsonContent.Create(new
            {
                from = settings.FromAddress,
                to = new[] { settings.RecipientAddress },
                subject = "New InvoiceTrucker early-access request",
                html = $"""
                    <h1>New early-access request</h1>
                    <p><strong>Name:</strong> {Encode(subscriber.FullName)}</p>
                    <p><strong>Email:</strong> {Encode(subscriber.Email)}</p>
                    <p><strong>Company:</strong> {companyName}</p>
                    <p><strong>Fleet size:</strong> {fleetSize}</p>
                    """,
                reply_to = subscriber.Email
            })
        };
        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            settings.ResendApiKey);

        using var response = await httpClient.SendAsync(
            request,
            cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
