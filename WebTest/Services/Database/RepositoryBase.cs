using WebTest.Models;
using WebTest.Services.Database.Interfaces;

namespace WebTest.Services.Database
{
    public abstract class RepositoryBase<T> : IRepository
        where T : ModelBase
    {
        protected DatabaseContext context = null!;

        public void AddContext(DatabaseContext context) => this.context = context;

        public void Save(T model)
        {
            context.InsertOrUpdate(model);
            context.SaveChanges();
        }

        public void Delete(T model)
        {
            context.Remove(model);
            context.SaveChanges();
        }

        public T? GetById(int id)
        {
            return context.Set<T>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
