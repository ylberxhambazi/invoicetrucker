namespace FleetForge.Api.Endpoints;

internal static class EnumQuery
{
    public static bool TryParse<TEnum>(
        string? value,
        string parameterName,
        Dictionary<string, string[]> errors,
        out TEnum? parsed)
        where TEnum : struct, Enum
    {
        parsed = null;

        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        if (Enum.TryParse<TEnum>(value, true, out var enumValue)
            && Enum.IsDefined(enumValue))
        {
            parsed = enumValue;
            return true;
        }

        errors[parameterName] =
        [
            $"Value must be one of: {string.Join(", ", Enum.GetNames<TEnum>())}."
        ];
        return false;
    }
}
