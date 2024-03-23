using Cron.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cron;

public sealed class CronScheduler(
    IServiceProvider serviceProvider,
    IEnumerable<CronRegistryEntry> cronJobs) : BackgroundService
{
    private readonly IServiceProvider serviceProvider = serviceProvider;
    private readonly IReadOnlyCollection<CronRegistryEntry> cronJobs = cronJobs.ToList();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var tickTimer = new PeriodicTimer(TimeSpan.FromSeconds(30));
        var runMap = new Dictionary<DateTime, List<Type>>();
        while (await tickTimer.WaitForNextTickAsync(stoppingToken))
        {
            RunActiveJobs(runMap, stoppingToken);
            runMap = GetJobRuns();
        }
    }

    private void RunActiveJobs(
        Dictionary<DateTime, List<Type>> runMap,
        CancellationToken stoppingToken)
    {
        var now = UtcNowMinutePrecision();
        if (!runMap.TryGetValue(now, out var currentRuns))
        {
            return;
        }

        foreach (var runType in currentRuns)
        {
            if (GetScopedService(runType) is ICronJob job)
            {
                Task.Factory.StartNew(() => job.Run(stoppingToken), stoppingToken);
            }
        }
    }

    private Dictionary<DateTime, List<Type>> GetJobRuns()
    {
        var utcNow = DateTime.UtcNow;
        var runMap = new Dictionary<DateTime, List<Type>>();
        foreach (var cron in cronJobs)
        {
            var runDates = cron.CrontabSchedule.GetNextOccurrences(utcNow, utcNow.AddMinutes(1));
            if (runDates is not null)
            {
                foreach (var runDate in runDates)
                {
                    if (runMap.TryGetValue(runDate, out var value))
                    {
                        value.Add(cron.Type);
                    }
                    else
                    {
                        runMap[runDate] = [cron.Type];
                    }
                }
            }
        }

        return runMap;
    }

    private object? GetScopedService(Type type)
    {
        return serviceProvider.CreateScope()
            .ServiceProvider
            .GetService(type);
    }

    private static DateTime UtcNowMinutePrecision()
    {
        var now = DateTime.UtcNow;

        return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
    }
}
