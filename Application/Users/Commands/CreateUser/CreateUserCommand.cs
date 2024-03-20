using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public required string Login { get; set; }

    public required string Password { get; set; }
}
