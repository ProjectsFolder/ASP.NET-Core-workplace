using Microsoft.EntityFrameworkCore.Infrastructure;
using WebTest.Domains.Auth.Handlers;

namespace WebTest.Jobs
{
    public class DeleteExpiredTokens(IServiceProvider serviceProvider) : ICronJob
    {
        public Task Run(CancellationToken token = default)
        {
            var scope = serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<Domains.Auth.Handlers.DeleteExpiredTokens>();
            handler?.Handle();

            return Task.CompletedTask;
        }
    }
}
