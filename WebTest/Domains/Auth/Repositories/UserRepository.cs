using WebTest.Models.OrgStructure;
using WebTest.Services.Database;

namespace WebTest.Domains.Auth.Repositories
{
    public class UserRepository : RepositoryBase
    {
        public User? GetUserByLogin(string username)
        {
            return context.Users.FirstOrDefault(u => string.Equals(u.Login.ToLower(), username.ToLower()));
        }
    }
}
