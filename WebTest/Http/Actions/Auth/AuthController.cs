using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.Auth.Handlers;
using WebTest.Dto.Auth.Request;
using WebTest.Security.Authentication.UserToken;

namespace WebTest.Http.Actions.Auth
{
    public class AuthController : AppController
    {
        [HttpPost]
        public IActionResult Login([FromBody] AuthDto request, Login handler)
        {
            return Success(handler, request);
        }

        [HttpPost]
        [Route("logout")]
        [Authorize(AuthenticationSchemes = UserTokenDefaults.SchemaName)]
        public IActionResult Logout(Logout handler)
        {
            return Success(handler);
        }
    }
}
