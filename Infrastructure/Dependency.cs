using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.EventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infrastructure;

public static class Dependency
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<ITransaction, TransactionHandler>();

        return services;
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
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

        var connection = factory.CreateConnection();
        services.AddSingleton(connection);
        services.AddHostedService<RabbitMqListener>();
        services.AddScoped<IRabbitMq, RabbitMq>();

        return services;
    }
}
