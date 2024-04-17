using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Api.Security.Authentication.UserToken;

public class UserTokenHandler(
    IOptionsMonitor<UserTokenOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    DatabaseContext dataContext) : AuthenticationHandler<UserTokenOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(Options.HeaderName, out StringValues token))
        {
            return AuthenticateResult.Fail($"Missing header: {Options.HeaderName}");
        }

        token = token.ToString()[7..];
        var userToken = await dataContext.Tokens.Include(t => t.User).FirstOrDefaultAsync(t => t.Value == token.ToString());
        if (userToken == null)
        {
            return AuthenticateResult.Fail("Invalid token.");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userToken.User.Login),
        };
        var claimsIdentity = new ClaimsIdentity(claims, UserTokenDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, UserTokenDefaults.AuthenticationScheme));
    }
}
