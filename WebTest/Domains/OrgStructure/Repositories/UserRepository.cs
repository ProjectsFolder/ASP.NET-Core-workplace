using WebTest.Models.OrgStructure;
using WebTest.Services.Database;

namespace WebTest.Domains.OrgStructure.Repositories
{
    public class UserRepository : BaseRepository
    {
        public IEnumerable<User> GetUsers() => context.Users;
    }
}
