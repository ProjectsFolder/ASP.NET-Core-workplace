using Microsoft.EntityFrameworkCore;
using WebTest.Services.Database;

namespace WebTest.Boot.Configure
{
    public static class ConfigureAppServices
    {
        private static readonly object locker = new();

        public static void DatabaseMigrate(this WebApplication application)
        {
            lock (locker)
            {
                var context = application.Services.GetService(typeof(DataContext)) as DataContext;
                context?.Database.Migrate();
            }
        }
    }
}
