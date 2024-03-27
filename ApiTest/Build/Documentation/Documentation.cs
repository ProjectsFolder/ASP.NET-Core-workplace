using Api.Build.Documentation.Filters;
using Api.Build.Documentation.Options;
using Api.Security.Authentication.UserToken;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Build.Documentation;

public static class Documentation
{
    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.AddSwaggerOptions();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddFilters();
            options.EnableAnnotations();
            options.AddSecurityDefinition(UserTokenDefaults.SchemaName, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = config.GetValue<string>("UserTokenHeaderName") ?? "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Application security",
            });
        });
    }

    private static void AddFilters(this SwaggerGenOptions options)
    {
        options.OperationFilter<MediaTypeFilter>();
        options.OperationFilter<AuthenticationFilter>();
    }

    private static void AddSwaggerOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerDocsOptions>();
    }
}
