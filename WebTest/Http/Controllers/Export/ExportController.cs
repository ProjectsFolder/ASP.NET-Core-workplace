using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Attributes.ActionFilter;
using WebTest.Security.Authentication.ApiToken;

namespace WebTest.Http.Controllers.Export
{
    [Authorize(AuthenticationSchemes = ApiTokenDefaults.SchemaName)]
    public class ExportController : AppController
    {
        [HttpGet]
        [PortFilter(port: 8082)]
        public IActionResult Export()
        {
            return Ok(new { port = HttpContext.Connection.LocalPort });
        }
    }
}
