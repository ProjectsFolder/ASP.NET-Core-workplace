using Microsoft.EntityFrameworkCore;
using WebTest.Models;
using WebTest.Models.Auth;
using WebTest.Models.Files;
using WebTest.Models.OrgStructure;

namespace WebTest.Services.Database
{
    public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Token> Tokens { get; set; } = null!;

        public DbSet<UserFile> Files { get; set; } = null!;

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
        }

        public T Transaction<T>(Func<T> func)
        {
            T? result;
            if (Database.CurrentTransaction == null)
            {
                using var transaction = Database.BeginTransaction();
                try
                {
                    result = func();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            else
            {
                result = func();
            }

            return result;
        }

        public void Transaction(Action action)
        {
            if (Database.CurrentTransaction == null)
            {
                using var transaction = Database.BeginTransaction();
                try
                {
                    action();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            else
            {
                action();
            }
        }
    }
}
