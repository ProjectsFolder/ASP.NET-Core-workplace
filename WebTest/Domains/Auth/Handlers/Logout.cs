using WebTest.Attributes;
using WebTest.Domains.Auth.Repositories;
using WebTest.Services;

namespace WebTest.Domains.Auth.Handlers
{
    [Dependency]
    public class Logout(
        AuthService authService,
        TokenRepository tokenRepository
        ) : IHandler<object, object>
    {
        public object? Handle(object? dto)
        {
            var user = authService.GetCurrentUser();
            if (user == null)
            {
                return null;
            }

            tokenRepository.DeleteAllByUser(user.Id);

            return null;
        }
    }
}
