using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Configure
{
    private static readonly object locker = new();

    public static void DatabaseMigrate(this IServiceProvider services)
    {
        lock (locker)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            context.Database.Migrate();
        }
    }
}
