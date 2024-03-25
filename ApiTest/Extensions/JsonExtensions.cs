using System.Text.Json;

namespace Api.Extensions;

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
}
