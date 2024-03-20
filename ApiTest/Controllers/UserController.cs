using Api.Requests.User;
using Api.Responses.Meta;
using Api.Responses.User;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class UserController : BaseController
{
    [HttpGet]
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
    public async Task<ActionResult> Create([FromBody] CreateUserRequest request)
    {
        var command = Mapper.Map<CreateUserCommand>(request);
        var userId = await Mediator.Send(command);

        return Success(userId);
    }
}
