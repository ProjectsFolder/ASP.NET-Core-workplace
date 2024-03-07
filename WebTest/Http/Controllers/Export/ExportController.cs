using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebTest.Attributes.ActionFilter;
using WebTest.Domains.Time.Interfaces;
using WebTest.Security.Authentication.ApiToken;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebTest.Http.Controllers.Export
{
    [Authorize(AuthenticationSchemes = ApiTokenDefaults.SchemaName)]
    public class ExportController : AppController
    {
        [HttpGet]
        public IActionResult Export(ITimeService timeService)
        {
            return Ok(new { time = timeService.GetTime() });
        }
    }
}
