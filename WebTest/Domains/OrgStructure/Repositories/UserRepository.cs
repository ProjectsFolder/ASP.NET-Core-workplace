using WebTest.Services;
using UserModel = WebTest.Models.OrgStructure.User;

namespace WebTest.Domains.OrgStructure.Repositories
{
    public class UserRepository : BaseRepository
    {
        public IEnumerable<UserModel> GetUsers() => context.Users;

        public UserModel? GetUser(int id) => context.Users.FirstOrDefault(u => u.Id == id);
    }
}
