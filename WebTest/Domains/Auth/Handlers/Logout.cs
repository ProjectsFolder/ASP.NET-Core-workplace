using WebTest.Domains.Auth.Repositories;
using WebTest.Domains.Interfaces;
using WebTest.Services;

namespace WebTest.Domains.Auth.Handlers
{
    public class Logout(
        AuthService authService,
        TokenRepository tokenRepository
        ) : ISimpleHandler
    {
        public void Handle()
        {
            var user = authService.GetCurrentUser();
            if (user != null)
            {
                tokenRepository.DeleteAllByUser(user.Id);
            }
        }
    }
}
