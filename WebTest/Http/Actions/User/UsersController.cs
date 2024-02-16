using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.User.Handlers;
using WebTest.Security.Authentication.UserToken;

namespace WebTest.Http.Actions.User
{
    [Authorize(AuthenticationSchemes = UserTokenDefaults.SchemaName)]
    public class UsersController : AppController
    {
        [HttpGet]
        public IActionResult List(GetList handler)
        {
            return Success(handler);
        }
    }
}
