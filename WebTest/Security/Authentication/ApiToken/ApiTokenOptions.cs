using Microsoft.AspNetCore.Authentication;

namespace WebTest.Security.Authentication.ApiToken
{
    public class ApiTokenOptions : AuthenticationSchemeOptions
    {
        public string HeaderName { get; set; } = "Authorization";

        public string? Token { get; set; }
    }
}
