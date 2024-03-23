namespace Cron.Interfaces;

public interface ICronJob
{
    void Run(CancellationToken token = default);
}
