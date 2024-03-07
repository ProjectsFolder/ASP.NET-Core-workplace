using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WebTest.Attributes.ActionFilter;
using WebTest.Services.Database;

namespace WebTest.Http.Controllers.Service
{
    [ServiceRoute]
    public class ServiceController : AppController
    {
        [HttpPost("migrate")]
        public IActionResult Migrate(DatabaseContext context, [FromQuery] string? target = null)
        {
            context.GetInfrastructure().GetRequiredService<IMigrator>().Migrate(target);
            var applied = context.Database.GetAppliedMigrations();

            return Ok(applied);
        }
    }
}
