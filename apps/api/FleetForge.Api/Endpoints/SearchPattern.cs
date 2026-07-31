namespace FleetForge.Api.Endpoints;

internal static class SearchPattern
{
    public const string EscapeCharacter = "\\";

    public static string Create(string search)
    {
        var escaped = search.Trim()
            .Replace("\\", "\\\\", StringComparison.Ordinal)
            .Replace("%", "\\%", StringComparison.Ordinal)
            .Replace("_", "\\_", StringComparison.Ordinal);

        return $"%{escaped}%";
    }
}
