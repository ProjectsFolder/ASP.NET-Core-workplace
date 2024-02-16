using WebTest.Services;
using UserModel = WebTest.Models.User.User;

namespace WebTest.Domains.User.Repositories
{
    public class UserRepository : BaseRepository
    {
        public IEnumerable<UserModel> GetUsers() => context.Users;

        public UserModel? GetUser(int id) => context.Users.FirstOrDefault(u => u.Id == id);
    }
}
