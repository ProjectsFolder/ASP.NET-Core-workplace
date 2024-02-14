using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.Time;
using WebTest.Exeptions.Concrete;
using WebTest.Http.Actions.Time.Request;
using WebTest.Security.Authentication.Token;

namespace WebTest.Http.Actions.Time
{
    public class TimeController : BaseAction
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = TokenAuthDefaults.SchemaName)]
        public IActionResult GetTime([FromBody] TimeRequest request, GetTimeHandler handler)
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
