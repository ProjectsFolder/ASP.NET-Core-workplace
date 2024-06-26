using Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Infrastructure.EventBus.Kafka;

public class Kafka : IKafka
{
    private readonly ProducerConfig _config;

    public Kafka(IConfiguration configuration)
    {
        _config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:Host"],
        };
    }

    public async void SendAsync(string exchange, object message)
    {
        var serialized = JsonSerializer.Serialize(message);
        await SendAsync(exchange, serialized);
    }

    public async Task SendAsync(string topic, string message)
    {
        using var client = new ProducerBuilder<Null, string>(_config).Build();
        _ = await client.ProduceAsync(topic, new Message<Null, string> { Value = message });
    }
}
