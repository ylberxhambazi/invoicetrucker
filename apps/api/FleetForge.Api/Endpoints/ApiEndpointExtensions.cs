namespace FleetForge.Api.Endpoints;

public static class ApiEndpointExtensions
{
    public static IEndpointRouteBuilder MapFleetForgeApi(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDashboardEndpoints();
        endpoints.MapTruckEndpoints();
        endpoints.MapDriverEndpoints();
        endpoints.MapClientEndpoints();
        endpoints.MapInvoiceEndpoints();
        endpoints.MapExpenseEndpoints();
        endpoints.MapReportEndpoints();
        endpoints.MapDocumentEndpoints();
        endpoints.MapSettingsEndpoints();
        endpoints.MapNewsletterEndpoints();

        return endpoints;
    }
}
