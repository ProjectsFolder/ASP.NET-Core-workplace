using Microsoft.AspNetCore.Mvc.Filters;
using WebTest.Exeptions.Concrete;

namespace WebTest.Attributes.ActionFilter
{
    public class PortFilterAttribute(int port) : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Connection.LocalPort != port)
            {
                throw new ApiException("Not found", 404);
            }
        }
    }
}
