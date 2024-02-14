using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace WebTest.Exeptions
{
    public class ProblemDetailsFactory : Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory
    {
        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null
        )
        {
            httpContext.Response.StatusCode = statusCode ?? (int)HttpStatusCode.InternalServerError;

            return new ProblemDetails()
            {
                Title = title,
            };
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null
        )
        {
            httpContext.Response.StatusCode = statusCode ?? (int)HttpStatusCode.BadRequest;

            var error = new ValidationProblemDetails()
            {
                Type = type,
                Title = title ?? "Validation error",
            };

            foreach (var field in modelStateDictionary)
            {
                error.Errors.Add(field.Key.ToLower(), field.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            }

            return error;
        }
    }
}
