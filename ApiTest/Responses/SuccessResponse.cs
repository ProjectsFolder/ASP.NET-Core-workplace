using System.Text.Json.Serialization;

namespace Api.Responses;

public class SuccessResponse<T>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Item { get; set; } = default;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<T>? Items { get; set; } = null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Meta { get; set; } = null;
}
