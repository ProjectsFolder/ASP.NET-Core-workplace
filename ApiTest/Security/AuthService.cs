using Application.Common.Attributes;
using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using System.Security.Claims;

namespace Api.Security
{
    [Dependency(baseType: typeof(ICurrentUser))]
    public sealed class AuthService(ClaimsPrincipal claims, DatabaseContext dataContext) : ICurrentUser
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
