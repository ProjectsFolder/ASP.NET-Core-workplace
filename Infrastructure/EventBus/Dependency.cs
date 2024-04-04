using Application.Interfaces;
using Application.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infrastructure.EventBus;

public static class Dependency
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(services =>
        {
            return Retry.Execute(() =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = configuration["Mq:Host"],
                    UserName = configuration["Mq:Login"],
                    Password = configuration["Mq:Password"],
                    DispatchConsumersAsync = true,
                };
                if (int.TryParse(configuration["Mq:Port"], out int tcpPort))
                {
                    factory.Port = tcpPort;
                }

                return factory.CreateConnection();
            }, 6, 5000);
        });
        services.AddHostedService<RabbitMqListener>();
        services.AddScoped<IRabbitMq, RabbitMq>();

        return services;
    }
}
