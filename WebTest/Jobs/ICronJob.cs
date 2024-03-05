namespace WebTest.Jobs
{
    public interface ICronJob
    {
        void Run(CancellationToken token = default);
    }
}
