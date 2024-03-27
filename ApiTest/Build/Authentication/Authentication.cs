using Api.Build.Authentication;
using Api.Security.Authentication.UserToken;

namespace Api.Build.Authentication;

public static class Authentication
{
    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        builder.Services.AddAuthentication().AddScheme<UserTokenOptions, UserTokenHandler>(UserTokenDefaults.SchemaName, options =>
        {
            options.HeaderName = config.GetValue<string>("UserTokenHeaderName") ?? "Authorization";
        });

        builder.Services.AddTransient(services =>
        {
            var service = services.GetService<IHttpContextAccessor>()?.HttpContext?.User;

            return service ?? throw new Exception("Service not found");
        });
    }
}
