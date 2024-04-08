using Cron.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Cron;

public static class Dependency
{
    public static IServiceCollection AddCronParser(this IServiceCollection services)
    {
        services.AddSingleton<ICronParser, CronParser>();

        return services;
    }
}
