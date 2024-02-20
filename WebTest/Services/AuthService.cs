using WebTest.Attributes;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebTest.Models.OrgStructure;

namespace WebTest.Services
{
    [Service]
    public sealed class AuthService(ClaimsPrincipal claims, DataContext dataContext)
    {
        public User? GetCurrentUser()
        {
            var login = claims.Identity?.Name;
            if (login == null)
            {
                return null;
            }

            return dataContext.Users.FirstOrDefault(u => string.Equals(u.Login.ToLower(), login.ToLower()));
        }

        public static string HashPassword(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();

            return passwordHasher.HashPassword(user, password);
        }

        public static bool CheckPassword(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
