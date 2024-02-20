using Microsoft.EntityFrameworkCore;
using WebTest.Models;
using WebTest.Models.Auth;
using WebTest.Models.OrgStructure;

namespace WebTest.Services.Database
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
            T? result;
            if (Database.CurrentTransaction == null)
            {
                using var transaction = Database.BeginTransaction();
                try
                {
                    result = func();
                    SaveChanges();
                    transaction.Commit();

                } catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            else
            {
                result = func();
                SaveChanges();
            }

            return result;
        }

        public void Transaction(Action action)
        {
            if (Database.CurrentTransaction == null)
            {
                using var transaction = Database.BeginTransaction();
                action();
                SaveChanges();
                try
                {
                    action();
                    SaveChanges();
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
                SaveChanges();
            }
        }
    }
}
