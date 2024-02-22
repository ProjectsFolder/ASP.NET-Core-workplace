using WebTest.Domains.Auth.Repositories;

namespace WebTest.Domains.Auth.Handlers
{
    public class DeleteExpiredToekns(
        TokenRepository tokenRepository
        ) : ISimpleHandler
    {
        public void Handle()
        {
            tokenRepository.DeleteExpired();
        }
    }
}
