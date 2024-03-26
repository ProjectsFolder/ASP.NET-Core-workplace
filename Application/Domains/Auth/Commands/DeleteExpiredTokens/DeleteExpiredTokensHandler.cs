using Application.Interfaces;
using Application.Specifications.Token;
using Domain;
using MediatR;

namespace Application.Domains.Auth.Commands.DeleteExpiredTokens;

public class DeleteExpiredTokensHandler(
    IRepository<Token> tokenRepository) : IRequestHandler<DeleteExpiredTokensCommand>
{
    public async Task Handle(DeleteExpiredTokensCommand request, CancellationToken cancellationToken)
    {
        var expiredTokens = new GetExpiredTokensSpecification(request.Seconds);
        var tokens = await tokenRepository.ListAsync(expiredTokens, cancellationToken);
        await tokenRepository.DeleteRangeAsync(tokens, cancellationToken);
    }
}
