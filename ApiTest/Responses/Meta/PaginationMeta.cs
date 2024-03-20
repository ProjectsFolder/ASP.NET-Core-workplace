using System.Text.Json.Serialization;

namespace Api.Responses.Meta;

public class PaginationMeta
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public required int PageCount { get; set; }
}
