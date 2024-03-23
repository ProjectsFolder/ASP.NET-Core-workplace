using NCrontab;

namespace Cron;

public sealed record CronRegistryEntry(
    Type Type,
    CrontabSchedule CrontabSchedule);
