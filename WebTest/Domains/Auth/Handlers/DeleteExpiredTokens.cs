using WebTest.Domains.Auth.Repositories;
using WebTest.Domains.Interfaces;
using WebTest.Services;

namespace WebTest.Domains.Auth.Handlers
{
    public class DeleteExpiredToekns(
        TokenRepository tokenRepository,
        ConfigService config
        ) : ISimpleHandler
    {
        public void Handle()
        {
            var seconds = config.Get<int>("UserTokenExpiresSeconds");
            tokenRepository.DeleteExpired(seconds);
        }
    }
}
