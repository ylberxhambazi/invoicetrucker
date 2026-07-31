namespace FleetForge.Api.Contracts.Common;

public static class QueryValidation
{
    public const int MaximumPageSize = 100;

    public static Dictionary<string, string[]> Validate(
        ListQuery query,
        IReadOnlySet<string> allowedSortFields)
    {
        var errors = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

        var page = query.Page ?? 1;
        var pageSize = query.PageSize ?? 20;

        if (page < 1)
        {
            errors["page"] = ["Page must be at least 1."];
        }

        if (pageSize is < 1 or > MaximumPageSize)
        {
            errors["pageSize"] =
            [
                $"Page size must be between 1 and {MaximumPageSize}."
            ];
        }

        var sortBy = string.IsNullOrWhiteSpace(query.SortBy)
            ? "name"
            : query.SortBy;
        var sortDirection = string.IsNullOrWhiteSpace(query.SortDirection)
            ? "asc"
            : query.SortDirection;

        if (!allowedSortFields.Contains(sortBy))
        {
            errors["sortBy"] =
            [
                $"Sort field must be one of: {string.Join(", ", allowedSortFields.Order())}."
            ];
        }

        if (!sortDirection.Equals("asc", StringComparison.OrdinalIgnoreCase)
            && !sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase))
        {
            errors["sortDirection"] = ["Sort direction must be 'asc' or 'desc'."];
        }

        if (query.Search?.Length > 120)
        {
            errors["search"] = ["Search cannot exceed 120 characters."];
        }

        return errors;
    }
}
