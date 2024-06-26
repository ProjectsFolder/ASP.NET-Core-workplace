using Application.Utils.JsonConverters;
using System.Text.Json.Serialization;

namespace Infrastructure.EventBus.Kafka.Dto;

internal record Event
{
    public required string Type { get; set; }

    [JsonConverter(typeof(UnsafeRawJsonConverter))]
    public required string Content { get; set; }
}
