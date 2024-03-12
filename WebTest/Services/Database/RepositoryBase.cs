using WebTest.Models;

namespace WebTest.Services.Database
{
    public abstract class RepositoryBase
    {
        protected DatabaseContext context = null!;

        public void AddContext(DatabaseContext context) => this.context = context;

        public void Save<T>(T model)
            where T : ModelBase
        {
            context.InsertOrUpdate(model);
            context.SaveChanges();
        }

        public void Delete<T>(T model)
            where T : ModelBase
        {
            context.Remove(model);
            context.SaveChanges();
        }

        public T? GetById<T>(int id)
            where T : ModelBase
        {
            return context.Set<T>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
