﻿using Cron.Interfaces;
using Cron.Jobs;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;
using System.Reflection;

namespace Cron;

public static class Dependency
{
    public static IServiceCollection AddCronJobs(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddCronJob<DeleteExpiredTokensJob>("* * * * *");

        return services;
    }

    private static void AddCronJob<T>(this IServiceCollection services, string cronExpression)
        where T : class, ICronJob
    {
        var cron = CrontabSchedule.TryParse(cronExpression)
                   ?? throw new ArgumentException("Invalid cron expression", nameof(cronExpression));

        services.AddHostedService<CronScheduler>();
        services.AddSingleton(new CronRegistryEntry(typeof(T), cron));
        services.AddScoped<T>();
    }
}
