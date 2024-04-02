using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

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

    public static async void DatabaseSeed(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;
        try
        {
            var context = scopedProvider.GetRequiredService<DatabaseContext>();
            var passwordHasher = scopedProvider.GetRequiredService<IPasswordHasher>();
            await DatabaseContextSeed.SeedAsync(context, passwordHasher);
        }
        catch
        {
            Console.WriteLine("An error occurred seeding the DB.");
        }
    }
}
