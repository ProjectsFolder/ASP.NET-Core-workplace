using Microsoft.AspNetCore.Mvc.Filters;
using WebTest.Exeptions.Concrete;
using WebTest.Http.Controllers;

namespace WebTest.Attributes.ActionFilter
{
    public class ServiceRouteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Connection.LocalPort != 8082)
            {
                throw new ApiException("Not found", 404);
            }
        }
    }
}
