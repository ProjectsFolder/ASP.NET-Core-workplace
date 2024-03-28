using System.Text.Json;

namespace Application.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static T? FromJson<T>(this string json) =>
        JsonSerializer.Deserialize<T>(json, jsonOptions);

    public static string ToJson<T>(this T obj) =>
        JsonSerializer.Serialize(obj, jsonOptions);

    public static bool TryParseJson(this string json, Type type, out object? result)
    {
        result = null;
        try
        {
            result = JsonSerializer.Deserialize(json, type);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
