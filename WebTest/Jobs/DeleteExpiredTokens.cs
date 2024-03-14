using WebTest.Services;
using Handler = WebTest.Domains.Auth.Handlers.DeleteExpiredTokens;

namespace WebTest.Jobs
{
    public class DeleteExpiredTokens(ContainerService containerService) : ICronJob
    {
        public void Run(CancellationToken token = default)
        {
            var handler = containerService.GetRequiredService<Handler>();
            handler.Handle();
        }
    }
}
