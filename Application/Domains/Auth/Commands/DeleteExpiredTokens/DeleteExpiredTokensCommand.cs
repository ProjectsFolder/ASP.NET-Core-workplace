using MediatR;

namespace Application.Domains.Auth.Commands.DeleteExpiredTokens;

public class DeleteExpiredTokensCommand : IRequest
{
    public required int TokenLifetimeSeconds { get; set; }
}
