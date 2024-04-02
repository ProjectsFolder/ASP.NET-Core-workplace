namespace Application.Interfaces;

public interface IRabbitMq
{
    Task SendAsync(string exchange, string routingKey, object message);
}
