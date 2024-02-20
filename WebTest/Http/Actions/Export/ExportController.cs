using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.Time.Interfaces;
using WebTest.Http.Actions;
using WebTest.Security.Authentication.ApiToken;

namespace WebTest.Http.Actions.Export
{
    [Authorize(AuthenticationSchemes = ApiTokenDefaults.SchemaName)]
    public class ExportController : AppController
    {
        [HttpGet]
        public IActionResult Export(ITimeService timeService)
        {
            return Ok(new { Time = timeService.GetTime() });
        }
    }
}
