using Application.Domains.Auth.Commands.DeleteExpiredTokens;
using Cron.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Cron.Jobs;

public class DeleteExpiredTokensJob(IMediator mediator, IConfiguration configuration) : ICronJob
{
    public async void Run(CancellationToken token = default)
    {
        var lifetime = 120;
        if (int.TryParse(configuration["TokenLifetimeSeconds"], out int seconds))
        {
            lifetime = seconds;
        }

        var command = new DeleteExpiredTokensCommand { Seconds = lifetime };
        await mediator.Send(command, token);
    }
}
