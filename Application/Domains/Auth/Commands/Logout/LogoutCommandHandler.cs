using Application.Interfaces;
using Application.Services;
using Application.Specifications.Token;
using Domain;
using MediatR;

namespace Application.Domains.Auth.Commands.Logout;

public class LogoutCommandHandler(
    AuthService authService,
    IRepository<Token> tokenRepository) : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = authService.GetCurrentUser();

        var tokensByUser = new TokensByUserSpecification(user.Id);
        var tokens = await tokenRepository.ListAsync(tokensByUser, cancellationToken);
        await tokenRepository.DeleteRangeAsync(tokens, cancellationToken);
    }
}
