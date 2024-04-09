using Api.Security.Authentication.UserToken;
using Application.Domains.Users.Commands.CreateUser;
using Application.Interfaces;
using Grpc;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Api.GrpcControllers;

[Authorize(AuthenticationSchemes = UserTokenDefaults.SchemaName)]
public class UserController(IMediator mediator, ITransaction transaction) : UserService.UserServiceBase
{
    public async override Task<CreateUsersResponse> Create(CreateUsersRequest request, ServerCallContext context)
    {
        var response = new CreateUsersResponse();
        await transaction.ExecuteAsync(async () =>
        {
            foreach (var user in request.Items)
            {
                var command = new CreateUserCommand
                {
                    Login = user.Login,
                    Password = user.Password,
                    Email = user.Email,
                };

                var userId = await mediator.Send(command);
                response.Ids.Add(userId);
            }
        });

        return await Task.FromResult(response);
    }
}
