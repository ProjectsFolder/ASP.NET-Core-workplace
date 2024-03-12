using System.Text.Json.Serialization;

namespace WebTest.Http.Responses
{
    public class SuccessDto<T>
        where T : class
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Item { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<T>? Items { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Meta { get; set; } = null;
    }
}
