using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Options;
using EventBus;
using EventBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using EventBus.Events;
using Application.Extensions;

namespace Infrastructure.EventBus;

public class RabbitMqListener(
    IServiceProvider serviceProvider,
    IOptions<Subscriptions> subscriptions) : BackgroundService
{
    private const string ExchangeName = "apitest_event_bus";
    private const string QueueName = "apitest_event_queue";

    private IConnection connection = null!;
    private IModel channel = null!;
    private AsyncEventingBasicConsumer consumer = null!;
    private readonly Subscriptions subscriptions = subscriptions.Value;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _ = Task.Factory.StartNew(() =>
        {
            connection = serviceProvider.GetRequiredService<IConnection>();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(
                exchange: ExchangeName,
                type: "direct");
            channel.QueueDeclare(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += OnMessageReceived;

            channel.BasicConsume(
                queue: QueueName,
                autoAck: false,
                consumer: consumer);

            foreach (var (eventName, _) in subscriptions.EventTypes)
            {
                channel.QueueBind(
                    queue: QueueName,
                    exchange: ExchangeName,
                    routingKey: eventName);
            }
        }, TaskCreationOptions.LongRunning);

        return Task.CompletedTask;
    }

    private async Task OnMessageReceived(object? sender, BasicDeliverEventArgs eventArgs)
    {
        var success = false;
        try
        {
            if (!subscriptions.EventTypes.TryGetValue(eventArgs.RoutingKey, out var eventType))
            {
                return;
            }

            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);
            if (!message.TryParseJson(eventType, out object? @event))
            {
                return;
            }

            var integrationEvent = @event as IntegrationEvent;
            if (integrationEvent == null)
            {
                return;
            }

            using var scope = serviceProvider.CreateAsyncScope();
            foreach (var handler in scope.ServiceProvider.GetKeyedServices<IIntegrationEventHandler>(eventType))
            {
                await handler.Handle(integrationEvent);
            }
            success = true;
        }
        finally
        {
            if (success)
            {
                channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
            else
            {
                channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: false);
            }
        }
    }

    public override void Dispose()
    {
        channel?.Dispose();
    }
}
