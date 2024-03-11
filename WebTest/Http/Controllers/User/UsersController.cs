using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.OrgStructure.Handlers;
using WebTest.Dto.File.Response;
using WebTest.Dto.OrgStructure.Command;
using WebTest.Dto.OrgStructure.Request;
using WebTest.Dto.OrgStructure.Response;
using WebTest.Http.Responses;
using WebTest.Security.Authentication.UserToken;

namespace WebTest.Http.Controllers.User
{
    [Authorize(AuthenticationSchemes = UserTokenDefaults.SchemaName)]
    public class UsersController : AppController
    {
        [HttpGet]
        [ProducesResponseType<SuccessItems<UserDto>>(200)]
        public IActionResult List(ListUsers handler)
        {
            return Success(handler);
        }

        [HttpPost]
        [ProducesResponseType<SuccessItem<UserDto>>(200)]
        public IActionResult Create(CreateDto request, CreateUser handler)
        {
            var command = new CreateCommand
            {
                Login = request.Login,
                Password = request.Password,
            };

            return Success(handler, command);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType<SuccessItem<UserDto>>(200)]
        public IActionResult Update(int id, UpdateDto request, UpdateUser handler)
        {
            var command = new UpdateCommand
            {
                Id = id,
                Login = request.Login,
                Password = request.Password,
            };

            return Success(handler, command);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, DeleteUser handler)
        {
            var command = new DeleteCommand { Id = id };

            return Success(handler, command);
        }
    }
}
