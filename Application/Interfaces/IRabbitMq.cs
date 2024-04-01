namespace Application.Interfaces;

public interface IRabbitMq
{
    Task SendMessageAsync(string exchange, string routingKey, object message);
}
