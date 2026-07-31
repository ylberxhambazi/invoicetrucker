namespace FleetForge.Api.Contracts.Common;

public class ListQuery
{
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public string? Search { get; init; }
    public string? SortBy { get; init; }
    public string? SortDirection { get; init; }
}
