namespace FleetForge.Api.Contracts.Common;

public sealed record PaginatedResponse<T>(
    IReadOnlyList<T> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages)
{
    public static PaginatedResponse<T> Create(
        IReadOnlyList<T> items,
        int page,
        int pageSize,
        int totalCount)
    {
        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PaginatedResponse<T>(
            items,
            page,
            pageSize,
            totalCount,
            totalPages);
    }
}
