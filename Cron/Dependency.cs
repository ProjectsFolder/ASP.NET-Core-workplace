using Cron.Interfaces;
using Cron.Jobs;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cron;

public static class Dependency
{
    public static IServiceCollection AddCronJobs(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.RegisterJobs();

        return services;
    }

    private static void RegisterJobs(this IServiceCollection services)
    {
        services.AddCronJob<DeleteExpiredTokensJob>("* * * * *");
    }

    private static void AddCronJob<T>(this IServiceCollection services, string cronExpression)
        where T : class, ICronJob
    {
        services.AddHostedService<CronScheduler>();
        services.AddSingleton(new CronRegistryEntry(typeof(T), cronExpression));
        services.AddScoped<T>();
    }
}
