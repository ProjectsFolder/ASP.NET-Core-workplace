using Application.Domains.Users.Commands.CreateUser;
using Application.Interfaces;
using EventBus.Interfaces;
using MediatR;

namespace EventBus.Events.MassCreateUsers;

internal class MassCreateUsersHandler(
    IMediator mediator,
    ITransaction transaction)
    : IIntegrationEventHandler<MassCreateUsersEvent>
{
    public async Task Handle(MassCreateUsersEvent @event)
    {
        await transaction.ExecuteAsync(() => Process(@event));
    }

    private async Task Process(MassCreateUsersEvent @event)
    {
        foreach (var user in @event.Users)
        {
            var command = new CreateUserCommand
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
            };
            await mediator.Send(command);
        }
    }
}
