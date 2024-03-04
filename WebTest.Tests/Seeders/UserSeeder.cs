using WebTest.Models.OrgStructure;
using WebTest.Services.Database;
using WebTest.Services;

namespace WebTest.Tests.Seeders
{
    internal static class UserSeeder
    {
        public static void Seed(DatabaseContext context)
        {
            var user = new User() { Login = "test", Email = "test@mail.com" };
            user.Password = AuthService.HashPassword(user, "test");
            context.Users.Add(user);
        }
    }
}
