using WebTest.Models.OrgStructure;
using WebTest.Services;
using WebTest.Services.Database;

namespace WebTest.Tests.Seeders
{
    internal static class BaseSeeder
    {
        public static void Seed(DataContext context)
        {
            UserSeeder.Seed(context);
        }
    }
}
