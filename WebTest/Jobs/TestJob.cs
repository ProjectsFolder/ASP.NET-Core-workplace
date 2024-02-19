using WebTest.Domains.Time.Interfaces;

namespace WebTest.Jobs
{
    public class TestJob(ITimeService timeService) : ICronJob
    {
        public Task Run(CancellationToken token = default)
        {
            Console.WriteLine(timeService.GetTime());

            return Task.CompletedTask;
        }
    }
}
