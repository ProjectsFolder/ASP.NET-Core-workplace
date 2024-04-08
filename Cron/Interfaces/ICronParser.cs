namespace Cron.Interfaces;

public interface ICronParser
{
    IEnumerable<DateTime> GetNextMinuteOccurrences(string cronExpression);
}
