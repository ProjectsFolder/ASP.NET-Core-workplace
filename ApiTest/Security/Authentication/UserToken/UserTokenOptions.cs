using Microsoft.AspNetCore.Authentication;

namespace Api.Security.Authentication.UserToken
{
    public class UserTokenOptions : AuthenticationSchemeOptions
    {
        public string HeaderName { get; set; } = "Authorization";
    }
}
