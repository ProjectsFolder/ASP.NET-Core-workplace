using WebTest.Models;

namespace WebTest.Services.Database
{
    public abstract class BaseRepository
    {
        protected DatabaseContext context = null!;

        public void AddContext(DatabaseContext context) => this.context = context;

        public void Save<T>(T model)
            where T : BaseModel
        {
            context.InsertOrUpdate(model);
            context.SaveChanges();
        }

        public void Delete<T>(T model)
            where T : BaseModel
        {
            context.Remove(model);
            context.SaveChanges();
        }

        public T? GetById<T>(int id)
            where T : BaseModel
        {
            return context.Set<T>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
