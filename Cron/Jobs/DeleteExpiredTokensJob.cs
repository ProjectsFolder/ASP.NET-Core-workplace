using Application.Domains.Auth.Commands.DeleteExpiredTokens;
using Cron.Interfaces;
using MediatR;

namespace Cron.Jobs;

public class DeleteExpiredTokensJob(IMediator mediator) : ICronJob
{
    public async void Run(CancellationToken token = default)
    {
        var command = new DeleteExpiredTokensCommand();
        await mediator.Send(command, token);
    }
}
