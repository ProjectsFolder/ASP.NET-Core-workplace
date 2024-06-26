using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Services.Mail;
using Application.Services.Mail.Dto;
using Application.Services.Mail.Dto.Template;
using Application.Specifications.Token;
using Application.Specifications.User;
using Application.Utils;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Domains.Auth.Commands.Login;

public class LoginCommandHandler(
    MailService mailer,
    ITransaction transaction,
    IPasswordHasher passwordHasher,
    IRabbitMq rabbitMq,
    IKafka kafka,
    IMapper mapper,
    IRepository<User> userRepository,
    IRepository<Token> tokenRepository) : IRequestHandler<LoginCommand, string>
{
    private const string ExchangeName = "apitest_event_bus_login_user";

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

        var users = await userRepository.ListAsync(cancellationToken);

        _ = rabbitMq.SendAsync(ExchangeName, "", new { user.Login, Time = DateTime.UtcNow });
        kafka.SendAsync(ExchangeName, new { user.Login, Time = DateTime.UtcNow });

        var message = new MailTemplateDto
        {
            Subject = "Login notification",
            Template = "User with login \"{{ user.Login }}\" logged.<br />All users:<br />{% for user in users %}{{ user.Login }}<br />{% endfor %}",
            TemplateParameters = new {
                user = mapper.Map<UserTemplateDto>(user),
                users = users.Select(mapper.Map<UserTemplateDto>).ToList(),
            },
            ReceiverAddress = "admin@mail.com",
        };
        _ = mailer.SendAsync(message, cancellationToken);

        return token.Value;
    }
}
