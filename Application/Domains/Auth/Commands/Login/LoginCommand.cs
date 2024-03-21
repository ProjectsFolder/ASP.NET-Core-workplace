using MediatR;

namespace Application.Domains.Auth.Commands.Login;

public class LoginCommand : IRequest<string>
{
    public required string Login { get; set; }

    public required string Password { get; set; }
}
