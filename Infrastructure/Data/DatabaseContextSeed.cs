using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DatabaseContextSeed
{
    public static async Task SeedAsync(DatabaseContext context, IPasswordHasher passwordHasher)
    {
        var adminLogin = "admin";
        var admin = await context.Users.FirstOrDefaultAsync(u => u.Login == adminLogin);
        if (admin != null)
        {
            return;
        }

        admin = new Domain.User()
        {
            Login = adminLogin,
        };

        await context.Users.AddAsync(admin);
        await context.SaveChangesAsync();

        admin.Password = passwordHasher.HashPassword(admin, "password");
        await context.SaveChangesAsync();
    }
}
