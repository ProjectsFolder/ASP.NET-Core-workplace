using Api.Responses;
using Application.Common.Exceptions;
using System.Net;

namespace Api.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is NotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
        else if (exception is AuthenticateException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        await context.Response.WriteAsJsonAsync(new ErrorResponse()
        {
            Message = exception.Message
        });
    }
}
