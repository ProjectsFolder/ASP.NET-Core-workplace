using Microsoft.EntityFrameworkCore.Infrastructure;
using Handler = WebTest.Domains.Auth.Handlers.DeleteExpiredTokens;

namespace WebTest.Jobs
{
    public class DeleteExpiredTokens(IServiceProvider serviceProvider) : ICronJob
    {
        public void Run(CancellationToken token = default)
        {
            var scope = serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<Handler>();
            handler?.Handle();
        }
    }
}
