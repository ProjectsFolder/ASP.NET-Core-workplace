namespace Cron;

public sealed record CronRegistryEntry(
    Type Type,
    string CronExpression);
