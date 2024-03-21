using Api.Requests.Auth;
using Api.Security.Authentication.UserToken;
using Application.Domains.Auth.Commands.Login;
using Application.Domains.Auth.Commands.Logout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthRequest request)
        {
            var command = Mapper.Map<LoginCommand>(request);
            var token = await Mediator.Send(command);

            return Success(token);
        }

        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = UserTokenDefaults.SchemaName)]
        public async Task<ActionResult> Logout()
        {
            var command = new LogoutCommand();
            await Mediator.Send(command);

            return Success();
        }
    }
}
