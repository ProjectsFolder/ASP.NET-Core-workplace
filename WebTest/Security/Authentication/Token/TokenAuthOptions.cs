using Microsoft.AspNetCore.Authentication;

namespace WebTest.Security.Authentication.Token
{
    public class TokenAuthOptions : AuthenticationSchemeOptions
    {
        public string HeaderName { get; set; } = "Authorization";

        public string? Token { get; set; }
    }
}
