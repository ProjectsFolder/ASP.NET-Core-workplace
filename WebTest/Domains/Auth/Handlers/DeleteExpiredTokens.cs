using WebTest.Domains.Auth.Repositories;
using WebTest.Domains.Interfaces;
using WebTest.Services;
using WebTest.Services.Mail;
using WebTest.Services.Mail.Dto;

namespace WebTest.Domains.Auth.Handlers
{
    public class DeleteExpiredTokens(
        TokenRepository tokenRepository,
        ConfigService config,
        MailService mailer
        ) : ISimpleHandler
    {
        public void Handle()
        {
            var seconds = config.Get<int>("UserTokenExpiresSeconds");
            var deleted = tokenRepository.DeleteExpired(seconds);

            var message = new Message(
                "Delete expired tokens",
                $"Deleted tokens: {deleted}",
                "admin@mail.com");
            mailer.Send(message, false);
        }
    }
}
