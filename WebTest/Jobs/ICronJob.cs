namespace WebTest.Jobs
{
    public interface ICronJob
    {
        Task Run(CancellationToken token = default);
    }
}
