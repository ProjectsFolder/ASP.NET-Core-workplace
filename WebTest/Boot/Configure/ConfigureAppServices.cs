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
                using var scope = application.Services.CreateScope();
                var context = scope.ServiceProvider.GetService<DatabaseContext>();
                context?.Database.Migrate();
            }
        }

        public static void RequestEnableBuffering(this WebApplication application)
        {
            application.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });
        }
    }
}
