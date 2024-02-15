using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.User.Handlers;

namespace WebTest.Http.Actions.User
{
    public class UsersController : AppController
    {
        [HttpGet]
        public IActionResult List(GetList handler)
        {
            return Success(handler);
        }
    }
}
