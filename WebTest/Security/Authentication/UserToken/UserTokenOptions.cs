using Microsoft.AspNetCore.Authentication;

namespace WebTest.Security.Authentication.UserToken
{
    public class UserTokenOptions : AuthenticationSchemeOptions
    {
        public string HeaderName { get; set; } = "Authorization";
    }
}
