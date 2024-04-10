using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Cache;

public static class Dependency
{
    public static IServiceCollection AddCache(this IServiceCollection services, string connection)
    {
        services.AddStackExchangeRedisCache(options => {
            options.Configuration = connection;
            options.InstanceName = "apitest";
        });
        services.AddSingleton<ICache, DistributedCache>();

        return services;
    }
}
