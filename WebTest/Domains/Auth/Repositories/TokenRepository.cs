using WebTest.Models.Auth;
using WebTest.Services.Database;

namespace WebTest.Domains.Auth.Repositories
{
    public class TokenRepository : RepositoryBase<Token>
    {
        public void DeleteAllByUser(int userId)
        {
            context.Tokens.RemoveRange(context.Tokens.Where(t => t.UserId == userId));
            context.SaveChanges();
        }

        public int DeleteExpired(int seconds)
        {
            var time = DateTime.UtcNow.AddSeconds(-seconds);
            var toBeRemoved = context.Tokens.Where(t => t.CreatedAt < time).ToList();
            var removedCount = toBeRemoved.Count;
            context.Tokens.RemoveRange(toBeRemoved);
            context.SaveChanges();

            return removedCount;
        }
    }
}
