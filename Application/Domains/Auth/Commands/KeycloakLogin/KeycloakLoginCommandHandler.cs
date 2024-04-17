using Application.Common.Exceptions;
using Application.Domains.Auth.Commands.KeycloakLogin.Dto;
using Application.Interfaces;
using Application.Services.Mail;
using Application.Services.Mail.Dto;
using Application.Services.Mail.Dto.Template;
using Application.Specifications.User;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Domains.Auth.Commands.KeycloakLogin;

public class KeycloakLoginCommandHandler(
    MailService mailer,
    IRabbitMq rabbitMq,
    IMapper mapper,
    IOpenApiAuth auth,
    IRepository<User> userRepository) : IRequestHandler<KeycloakLoginCommand, TokenDto>
{
    private const string ExchangeName = "apitest_event_bus_login_user";

    public async Task<TokenDto> Handle(KeycloakLoginCommand request, CancellationToken cancellationToken)
    {
        var token = await auth.GetTokenAsync(request.Login, request.Password, cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.Login);

        var userbyLogin = new UserByLoginSpecification(request.Login);
        var user = await userRepository.FirstOrDefaultAsync(userbyLogin, cancellationToken);
        if (user == null)
        {
            user = new User
            {
                Login = request.Login,
                Password = "none",
            };
            await userRepository.AddAsync(user, cancellationToken);
        }

        var users = await userRepository.ListAsync(cancellationToken);

        _ = rabbitMq.SendAsync(ExchangeName, "", new { user.Login, Time = DateTime.UtcNow });

        var message = new MailTemplateDto
        {
            Subject = "Login notification",
            Template = "User with login \"{{ user.Login }}\" logged.<br />All users:<br />{% for user in users %}{{ user.Login }}<br />{% endfor %}",
            TemplateParameters = new
            {
                user = mapper.Map<UserTemplateDto>(user),
                users = users.Select(mapper.Map<UserTemplateDto>).ToList(),
            },
            ReceiverAddress = "admin@mail.com",
        };
        _ = mailer.SendAsync(message, cancellationToken);

        return mapper.Map<TokenDto>(token);
    }
}
