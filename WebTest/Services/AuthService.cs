using System.Security.Claims;
using WebTest.Models.User;

namespace WebTest.Services
{
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
    }
}
