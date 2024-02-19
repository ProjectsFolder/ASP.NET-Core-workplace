using WebTest.Attributes;
using System.Security.Claims;
using WebTest.Models.User;

namespace WebTest.Services
{
    [Service]
    public class AuthService(
        ClaimsPrincipal claims,
        DataContext dataContext)
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

        public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool CheckPassword(User user, string password) => BCrypt.Net.BCrypt.Verify(password, user.Password);
    }
}
