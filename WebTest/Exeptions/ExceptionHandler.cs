using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebTest.Exeptions.Concrete;

namespace WebTest.Exeptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var response = new ProblemDetails
            {
                Title = exception.Message,
            };

            if (exception is ApiException apiException)
            {
                httpContext.Response.StatusCode = apiException.Status;
            }

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
