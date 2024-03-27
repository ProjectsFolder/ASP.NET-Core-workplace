using Api.Requests.User;
using Api.Responses.Documentation;
using Api.Responses.Meta;
using Api.Responses.User;
using Api.Security.Authentication.UserToken;
using Application.Domains.Users.Commands.CreateUser;
using Application.Domains.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(AuthenticationSchemes = UserTokenDefaults.SchemaName)]
public class UserController : BaseController
{
    [HttpGet]
    [ProducesResponseType<SuccessItemsWithMeta<UserResponse, PaginationMeta>>(200)]
    public async Task<ActionResult> List(ListUsersRequest request)
    {
        var query = Mapper.Map<GetUsersPaginatedQuery>(request);
        var result = await Mediator.Send(query);
        var response = result.Items.Select(Mapper.Map<UserResponse>).ToList();
        var meta = new PaginationMeta
        {
            PageCount = result.TotalPages,
        };

        return Success(response, meta);
    }

    [HttpPost]
    [ProducesResponseType<SuccessItem<int>>(200)]
    public async Task<ActionResult> Create([FromBody] CreateUserRequest request)
    {
        var command = Mapper.Map<CreateUserCommand>(request);
        var userId = await Mediator.Send(command);

        return Success(userId);
    }
}
