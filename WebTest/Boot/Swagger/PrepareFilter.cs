using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net.Mime;

namespace WebTest.Boot.Swagger
{
    internal class PrepareFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var key in operation.Responses.Keys)
            {
                FilterMediaTypes(operation.Responses[key].Content);
            }

            if (operation.RequestBody?.Content != null)
            {
                FilterMediaTypes(operation.RequestBody.Content);
            }

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

        private static void FilterMediaTypes(IDictionary<string, OpenApiMediaType> apiMediaTypes)
        {
            if (apiMediaTypes.TryGetValue(MediaTypeNames.Application.Json, out OpenApiMediaType? applicationJson))
            {
                apiMediaTypes.Clear();
                apiMediaTypes.Add(MediaTypeNames.Application.Json, applicationJson);
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
}
