using WebTest.Services;

namespace WebTest.Domains.Auth.Repositories
{
    public class UserRepository : BaseRepository
    {
        public Models.User.User? GetUserByLogin(string username)
        {
            return context.Users.FirstOrDefault(u => string.Equals(u.Login.ToLower(), username.ToLower()));
        }
    }
}
