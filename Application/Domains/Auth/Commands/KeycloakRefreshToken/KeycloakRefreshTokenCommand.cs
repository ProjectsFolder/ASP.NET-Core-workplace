using Application.Domains.Auth.Commands.KeycloakRefreshToken.Dto;
using MediatR;

namespace Application.Domains.Auth.Commands.KeycloakRefreshToken;

public class KeycloakRefreshTokenCommand : IRequest<TokenDto>
{
    public required string RefreshToken { get; set; }
}
