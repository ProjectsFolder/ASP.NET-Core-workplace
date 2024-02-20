using Microsoft.EntityFrameworkCore;
using WebTest.Models;
using WebTest.Models.Auth;
using WebTest.Models.OrgStructure;

namespace WebTest.Services
{
    public sealed class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Token> Tokens { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.Migrate();
        }

        public void InsertOrUpdate<T>(T model)
            where T : BaseModel
        {
            var set = Set<T>();

            if (model.Id > 0 && set.Any(e => e.Id == model.Id))
            {
                set.Attach(model);
                Entry(model).State = EntityState.Modified;
            }
            else
            {
                set.Add(model);
            }

            SaveChanges();
        }

        public T Transaction<T>(Func<T> func)
        {
            using var transaction = Database.BeginTransaction();
            var result = func();
            SaveChanges();
            transaction.Commit();

            return result;
        }

        public void Transaction(Action action)
        {
            using var transaction = Database.BeginTransaction();
            action();
            SaveChanges();
            transaction.Commit();
        }
    }
}
