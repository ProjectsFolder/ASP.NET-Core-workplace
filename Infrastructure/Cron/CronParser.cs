using Cron.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using NCrontab;
using System.Collections.Concurrent;

namespace Infrastructure.Cron;

public sealed class CronParser : ICronParser
{
    private readonly MemoryCache cache = new(new MemoryCacheOptions());

    private readonly ConcurrentDictionary<string, Semaphore> locks = [];

    public IEnumerable<DateTime> GetNextMinuteOccurrences(string cronExpression)
    {
        if (!cache.TryGetValue(cronExpression, out var cron))
        {
            var locker = locks.GetOrAdd(cronExpression, k => new Semaphore(1, 1));
            locker.WaitOne();
            try
            {
                if (!cache.TryGetValue(cronExpression, out cron))
                {
                    cron = CrontabSchedule.TryParse(cronExpression)
                        ?? throw new ArgumentException("Invalid cron expression", nameof(cronExpression));
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(86400));
                    cache.Set(cronExpression, cron, cacheOptions);
                }
            }
            finally
            {
                locker.Release();
            }
        }

        if (cron is CrontabSchedule schedule)
        {
            var baseTime = DateTime.UtcNow;

            return schedule.GetNextOccurrences(baseTime, baseTime.AddMinutes(1));
        }

        throw new Exception("CronParser exception");
    }
}
