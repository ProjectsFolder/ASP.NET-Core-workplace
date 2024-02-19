using NCrontab;

namespace WebTest.Services.Jobs
{
    public sealed record CronRegistryEntry(
        Type Type,
        CrontabSchedule CrontabSchedule);
}
