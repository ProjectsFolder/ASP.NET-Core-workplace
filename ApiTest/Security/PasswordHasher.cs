using Application.Common.Attributes;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Api.Security
{
    [Dependency(baseType: typeof(IPasswordHasher))]
    public class PasswordHasher : IPasswordHasher
    {
        public bool CheckPassword(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            return result == PasswordVerificationResult.Success;
        }

        public string HashPassword(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();

            return passwordHasher.HashPassword(user, password);
        }
    }
}
