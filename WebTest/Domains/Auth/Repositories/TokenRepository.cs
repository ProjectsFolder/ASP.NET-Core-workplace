using WebTest.Services.Database;

namespace WebTest.Domains.Auth.Repositories
{
    public class TokenRepository : BaseRepository
    {
        public void DeleteAllByUser(int userId)
        {
            context.Tokens.RemoveRange(context.Tokens.Where(t => t.UserId == userId));
            context.SaveChanges();
        }

        public void DeleteExpired()
        {
            var time = DateTime.UtcNow.AddSeconds(-1 * 60 * 10);
            context.Tokens.RemoveRange(context.Tokens.Where(t => t.CreatedAt < time));
            context.SaveChanges();
        }
    }
}
