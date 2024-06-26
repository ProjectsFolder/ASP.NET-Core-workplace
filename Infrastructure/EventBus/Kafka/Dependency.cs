using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventBus.Kafka;

public static class Dependency
{
    public static IServiceCollection AddKafka(this IServiceCollection services)
    {
        services.AddHostedService<KafkaListener>();
        services.AddScoped<IKafka, Kafka>();

        return services;
    }
}
