using Api.Requests.Auth;
using Api.Responses.Documentation;
using Api.Security.Authentication.UserToken;
using Application.Domains.Auth.Commands.KeycloakLogin;
using Application.Domains.Auth.Commands.KeycloakLogin.Dto;
using Application.Domains.Auth.Commands.KeycloakRefreshToken;
using Application.Domains.Auth.Commands.Login;
using Application.Domains.Auth.Commands.Logout;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiVersion(1.0)]
public class AuthController : BaseController
{
    [HttpPost("login")]
    [ProducesResponseType<SuccessItem<string>>(200)]
    public async Task<ActionResult> Login([FromBody] AuthRequest request)
    {
        var command = Mapper.Map<LoginCommand>(request);
        var token = await Mediator.Send(command);

        return Success(token);
    }

    [HttpPost("logout")]
    [Authorize(AuthenticationSchemes = UserTokenDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Logout()
    {
        var command = new LogoutCommand();
        await Mediator.Send(command);

        return Success();
    }


    [HttpPost("keycloak/login")]
    [ProducesResponseType<SuccessItem<TokenDto>>(200)]
    public async Task<ActionResult> KeycloakLogin([FromBody] AuthRequest request)
    {
        var command = Mapper.Map<KeycloakLoginCommand>(request);
        var token = await Mediator.Send(command);

        return Success(token);
    }

    [HttpPost("keycloak/refresh")]
    [ProducesResponseType<SuccessItem<TokenDto>>(200)]
    public async Task<ActionResult> KeycloakRefreshToken([FromBody] RefreshTokenRequest request)
    {
        var command = Mapper.Map<KeycloakRefreshTokenCommand>(request);
        var token = await Mediator.Send(command);

        return Success(token);
    }
}
