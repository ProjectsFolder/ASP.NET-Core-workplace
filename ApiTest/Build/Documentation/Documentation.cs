using Api.Build.Documentation.Filters;
using Api.Security.Authentication.UserToken;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Build.Documentation;

public static class Documentation
{
    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddFilters();
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
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
}
