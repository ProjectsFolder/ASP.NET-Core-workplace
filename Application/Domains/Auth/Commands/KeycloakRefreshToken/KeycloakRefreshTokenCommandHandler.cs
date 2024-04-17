using Application.Common.Exceptions;
using Application.Domains.Auth.Commands.KeycloakRefreshToken.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Domains.Auth.Commands.KeycloakRefreshToken;

public class KeycloakRefreshTokenCommandHandler(
    IMapper mapper,
    IOpenApiAuth auth) : IRequestHandler<KeycloakRefreshTokenCommand, TokenDto>
{
    public async Task<TokenDto> Handle(KeycloakRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await auth.RefreshTokenAsync(request.RefreshToken, cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.RefreshToken);

        return mapper.Map<TokenDto>(token);
    }
}
