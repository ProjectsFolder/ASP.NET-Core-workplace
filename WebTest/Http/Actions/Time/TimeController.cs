using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.Time.Handlers;
using WebTest.Dto.Time.Request;
using WebTest.Exeptions.Concrete;
using WebTest.Security.Authentication.Token;

namespace WebTest.Http.Actions.Time
{
    public class TimeController : AppController
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = TokenAuthDefaults.SchemaName)]
        public IActionResult GetTime([FromBody] TimeDto request, GetTime handler)
        {
            return Success(handler, request);
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            throw new ApiException("test1", 401);
        }
    }
}
