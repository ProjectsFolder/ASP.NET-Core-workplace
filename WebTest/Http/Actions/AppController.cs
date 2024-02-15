using Microsoft.AspNetCore.Mvc;
using WebTest.Domains;
using WebTest.Http.Responses;

namespace WebTest.Http.Actions
{
    [Route("api/[controller]")]
    public class AppController : ControllerBase
    {
        protected IActionResult Success<Request, Response>(IHandler<Request, Response> handler, Request? request = null)
            where Request : class
            where Response : class
        {
            var result = handler.Handle(request);

            var response = new SuccessDto();
            if (result is IEnumerable<object> items)
            {
                response.Items = items;
            } else
            {
                response.Item = result;
            }

            return Ok(response);
        }
    }
}
