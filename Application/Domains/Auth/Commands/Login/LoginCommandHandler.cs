using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Specifications.Token;
using Application.Specifications.User;
using Application.Utils;
using Domain;
using MediatR;

namespace Application.Domains.Auth.Commands.Login;

public class LoginCommandHandler(
    ITransaction transaction,
    IPasswordHasher passwordHasher,
    IRepository<User> userRepository,
    IRepository<Token> tokenRepository)
    : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await transaction.ExecuteAsync(() => Process(request, cancellationToken), cancellationToken);
    }

    private async Task<string> Process(LoginCommand request, CancellationToken cancellationToken)
    {
        var userbyLogin = new UserByLoginSpecification(request.Login);
        var user = await userRepository.FirstOrDefaultAsync(userbyLogin, cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.Login);

        if (!passwordHasher.CheckPassword(user, request.Password))
        {
            throw new AuthenticateException("Incorrect password");
        }

        var tokensByUser = new TokensByUserSpecification(user.Id);
        var tokens = await tokenRepository.ListAsync(tokensByUser, cancellationToken);
        await tokenRepository.DeleteRangeAsync(tokens, cancellationToken);

        var token = new Token()
        {
            CreatedAt = DateTime.UtcNow,
            UserId = user.Id,
            Value = StringUtils.RandomString(64)
        };

        await tokenRepository.AddAsync(token, cancellationToken);

        return token.Value;
    }
}
