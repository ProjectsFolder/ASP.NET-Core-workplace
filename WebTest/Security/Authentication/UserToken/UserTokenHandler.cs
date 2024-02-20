using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Encodings.Web;
using WebTest.Services.Database;

namespace WebTest.Security.Authentication.UserToken
{
    public class UserTokenHandler(IOptionsMonitor<UserTokenOptions> options, ILoggerFactory logger, UrlEncoder encoder, DataContext dataContext)
        : AuthenticationHandler<UserTokenOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(Options.HeaderName, out StringValues token))
            {
                return Task.FromResult(AuthenticateResult.Fail($"Missing header: {Options.HeaderName}"));
            }

            token = token.ToString()[7..];
            var userToken = dataContext.Tokens.Include(t => t.User).FirstOrDefault(t => t.Value == token.ToString());
            if (userToken == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token."));
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userToken.User.Login)
            };
            var claimsIdentity = new ClaimsIdentity(claims, UserTokenDefaults.SchemaName);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, UserTokenDefaults.SchemaName)));
        }
    }
}
