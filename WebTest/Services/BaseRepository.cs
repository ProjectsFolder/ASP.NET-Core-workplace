using WebTest.Models;

namespace WebTest.Services
{
    public class BaseRepository
    {
        protected DataContext context = null!;

        public void AddContext(DataContext context) => this.context = context;

        public void InsertOrUpdate<T>(T model)
            where T : class, IModel => context.InsertOrUpdate(model);
    }
}
