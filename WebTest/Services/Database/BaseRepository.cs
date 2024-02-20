using WebTest.Models;

namespace WebTest.Services.Database
{
    public class BaseRepository
    {
        protected DataContext context = null!;

        public void AddContext(DataContext context) => this.context = context;

        public void Save<T>(T model)
            where T : BaseModel => context.InsertOrUpdate(model);

        public void Delete<T>(T model)
            where T : BaseModel
        {
            context.Remove(model);
            context.SaveChanges();
        }
    }
}
