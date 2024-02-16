using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace WebTest.Security.Authentication.ApiToken
{
    public class ApiTokenHandler(IOptionsMonitor<ApiTokenOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : AuthenticationHandler<ApiTokenOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var appToken = Options.Token;
            if (appToken == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Token not setted."));
            }

            if (!Request.Headers.TryGetValue(Options.HeaderName, out StringValues token))
            {
                return Task.FromResult(AuthenticateResult.Fail($"Missing header: {Options.HeaderName}"));
            }

            if (token != appToken)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token."));
            }

            var claimsIdentity = new ClaimsIdentity(ApiTokenDefaults.SchemaName);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, ApiTokenDefaults.SchemaName)));
        }
    }
}
