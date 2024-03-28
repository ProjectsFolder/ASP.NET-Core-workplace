using Application.Domains.Auth.Commands.DeleteExpiredTokens;
using EventBus.Interfaces;
using MediatR;

namespace EventBus.Events.DeleteExpiredTokens;

internal class DeleteExpiredTokensHandler(IMediator mediator) : IIntegrationEventHandler<DeleteExpiredTokensEvent>
{
    public async Task Handle(DeleteExpiredTokensEvent @event)
    {
        var command = new DeleteExpiredTokensCommand { TokenLifetimeSeconds = @event.TokenLifetimeSeconds };
        await mediator.Send(command);
    }
}
