using WebTest.Models;

namespace WebTest.Services.Database
{
    public class BaseRepository
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
    }
}
