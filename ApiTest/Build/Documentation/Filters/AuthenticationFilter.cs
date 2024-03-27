using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Build.Documentation.Filters;

public class AuthenticationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authAttributes = context.MethodInfo?.DeclaringType?.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>();

        if (authAttributes != null && authAttributes.Any())
        {
            if (!operation.Responses.ContainsKey("401"))
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            }
            foreach (var authAttribute in authAttributes)
            {
                if (authAttribute.AuthenticationSchemes == null)
                {
                    continue;
                }

                var securityRequirement = GetSecurityRequirement(authAttribute.AuthenticationSchemes);
                operation.Security.Add(securityRequirement);
            }
        }
    }

    private static OpenApiSecurityRequirement GetSecurityRequirement(string schemaName)
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = schemaName,
            },
        };

        var securityRequirement = new OpenApiSecurityRequirement
            {
                { securityScheme, [schemaName] }
            };

        return securityRequirement;
    }
}
