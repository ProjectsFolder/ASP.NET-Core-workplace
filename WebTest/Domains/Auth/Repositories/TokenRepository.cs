using WebTest.Models.Auth;
using WebTest.Services;

namespace WebTest.Domains.Auth.Repositories
{
    public class TokenRepository : BaseRepository
    {
        public void DeleteAllByUser(int userId)
        {
            context.Tokens.RemoveRange(context.Tokens.Where(t => t.UserId == userId));
            context.SaveChanges();
        }
    }
}
