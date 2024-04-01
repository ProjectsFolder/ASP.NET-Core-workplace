using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Application.Interfaces;

namespace Infrastructure.EventBus;

public class RabbitMq(IConnection connection) : IRabbitMq
{
    public Task SendMessageAsync(string exchange, string routingKey, object message)
    {
        var serialized = JsonSerializer.Serialize(message);

        return SendMessageAsync(exchange, routingKey, serialized);
    }

    public Task SendMessageAsync(string exchange, string routingKey, string message)
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
