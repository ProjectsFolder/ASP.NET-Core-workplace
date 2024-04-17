using Application.Domains.Auth.Commands.KeycloakLogin.Dto;
using MediatR;

namespace Application.Domains.Auth.Commands.KeycloakLogin;

public class KeycloakLoginCommand : IRequest<TokenDto>
{
    public required string Login { get; set; }

    public required string Password { get; set; }
}
