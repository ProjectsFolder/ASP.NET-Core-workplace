using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.Auth.Handlers;
using WebTest.Dto.Auth.Command;
using WebTest.Dto.Auth.Request;
using WebTest.Dto.Auth.Response;
using WebTest.Http.Responses;
using WebTest.Security.Authentication.UserToken;

namespace WebTest.Http.Controllers.Auth
{
    public class AuthController : AppController
    {
        [HttpPost]
        [ProducesResponseType<SuccessItem<TokenDto>>(200)]
        public IActionResult Login([FromBody] AuthDto request, Login handler)
        {
            var command = new AuthCommand(request.Login, request.Password);

            return Success(handler, command);
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
