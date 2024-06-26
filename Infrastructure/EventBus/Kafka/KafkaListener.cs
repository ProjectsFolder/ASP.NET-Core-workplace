using Application.Extensions;
using Confluent.Kafka;
using EventBus;
using EventBus.Events;
using EventBus.Interfaces;
using Infrastructure.EventBus.Kafka.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventBus.Kafka;

public class KafkaListener(
    IConfiguration configuration,
    IOptions<Subscriptions> subscriptions,
    IServiceProvider serviceProvider) : BackgroundService
{
    private const string TopicName = "apitest_event_bus";

    private readonly IConfiguration _configuration = configuration;
    private readonly Subscriptions _subscriptions = subscriptions.Value;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _ = Task.Factory.StartNew(() =>
        {
            Listener(stoppingToken);
        }, stoppingToken);

        return Task.CompletedTask;
    }

    private async void Listener(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _configuration["Kafka:Host"],
            GroupId = "test",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe(TopicName);
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(stoppingToken);
                if (consumeResult != null)
                {
                    try
                    {
                        var kafkaMessage = consumeResult.Message.Value;
                        if (!kafkaMessage.TryParseJson(typeof(Event), out object? kafkaData))
                        {
                            continue;
                        }

                        var kafkaEvent = kafkaData as Event;
                        if (!_subscriptions.EventTypes.TryGetValue(kafkaEvent!.Type, out var eventType))
                        {
                            continue;
                        }

                        if (!kafkaEvent.Content.TryParseJson(eventType, out object? @event))
                        {
                            continue;
                        }

                        var integrationEvent = @event as IntegrationEvent;
                        if (integrationEvent == null)
                        {
                            continue;
                        }

                        using var scope = _serviceProvider.CreateAsyncScope();
                        foreach (var handler in scope.ServiceProvider.GetKeyedServices<IIntegrationEventHandler>(eventType))
                        {
                            await handler.Handle(integrationEvent);
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
    }
}
