using WebTest.Services;
using UserModel = WebTest.Models.User;

namespace WebTest.Domains.User.Repositories
{
    public class UserRepository : BaseRepository
    {
        public IEnumerable<UserModel> GetUsers() => context.Users;
    }
}
