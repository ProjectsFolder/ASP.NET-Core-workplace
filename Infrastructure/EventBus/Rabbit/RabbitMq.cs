using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Application.Interfaces;

namespace Infrastructure.EventBus.Rabbit;

public class RabbitMq(IConnection connection) : IRabbitMq
{
    public Task SendAsync(string exchange, string routingKey, object message)
    {
        var serialized = JsonSerializer.Serialize(message);

        return SendAsync(exchange, routingKey, serialized);
    }

    public Task SendAsync(string exchange, string routingKey, string message)
    {
        return Task.Run(() =>
        {
            var body = Encoding.UTF8.GetBytes(message);

            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(
                exchange: exchange,
                type: "direct");

            channel.BasicPublish(exchange: exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: body);
        });
    }
}
