using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Security.Authentication.ApiToken;

namespace WebTest.Http.Actions.Test
{
    [Authorize(AuthenticationSchemes = ApiTokenDefaults.SchemaName)]
    public class ExportController : AppController
    {
        [HttpGet]
        public IActionResult List()
        {
            return Ok("Export data");
        }
    }
}
