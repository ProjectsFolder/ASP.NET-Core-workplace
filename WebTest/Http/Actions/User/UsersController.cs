using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.OrgStructure.Handlers;
using WebTest.Dto.User.Request;
using WebTest.Security.Authentication.UserToken;

namespace WebTest.Http.Actions.User
{
    [Authorize(AuthenticationSchemes = UserTokenDefaults.SchemaName)]
    public class UsersController : AppController
    {
        [HttpGet]
        public IActionResult List(ListUsers handler)
        {
            return Success(handler);
        }

        [HttpPost]
        public IActionResult Create(CreateDto request, CreateUser handler)
        {
            return Success(handler, request);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, UpdateDto request, UpdateUser handler)
        {
            request.Id = id;

            return Success(handler, request);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, DeleteUser handler)
        {
            var request = new DeleteDto { Id = id };

            return Success(handler, request);
        }
    }
}
