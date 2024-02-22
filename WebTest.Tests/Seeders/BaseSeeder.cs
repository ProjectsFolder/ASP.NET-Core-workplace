using WebTest.Services.Database;

namespace WebTest.Tests.Seeders
{
    internal static class BaseSeeder
    {
        public static void Seed(DatabaseContext context)
        {
            UserSeeder.Seed(context);
        }
    }
}
