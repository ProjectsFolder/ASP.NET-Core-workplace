using Cron.Interfaces;
using NCrontab;

namespace Infrastructure.Cron;

public sealed class CronParser : ICronParser
{
    private readonly Dictionary<string, CrontabSchedule> expressions = [];

    private readonly object locker = new();

    public IEnumerable<DateTime> GetNextMinuteOccurrences(string cronExpression)
    {
        if (!expressions.TryGetValue(cronExpression, out var cron))
        {
            lock (locker)
            {
                if (!expressions.TryGetValue(cronExpression, out cron))
                {
                    cron = CrontabSchedule.TryParse(cronExpression)
                        ?? throw new ArgumentException("Invalid cron expression", nameof(cronExpression));
                    expressions[cronExpression] = cron;
                }
            }
        }
        
        var baseTime = DateTime.UtcNow;

        return cron.GetNextOccurrences(baseTime, baseTime.AddMinutes(1));
    }
}
