using MediatR;

namespace Application.Domains.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public required string Login { get; set; }

    public required string Password { get; set; }

    public string? Email { get; set; }
}
