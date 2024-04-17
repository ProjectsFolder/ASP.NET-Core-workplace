using Api.Security.Authentication.UserToken;
using Microsoft.IdentityModel.Tokens;

namespace Api.Build.Authentication;

public static class Authentication
{
    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        builder.Services.AddAuthentication().AddScheme<UserTokenOptions, UserTokenHandler>(UserTokenDefaults.AuthenticationScheme, options =>
        {
            options.HeaderName = config.GetValue<string>("UserTokenHeaderName") ?? "Authorization";
        });
        builder.Services.AddAuthentication()
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = Convert.ToBoolean($"{config["Keycloak:RequireHttps"]}");
                x.MetadataAddress = $"{config["Keycloak:ServerUrl"]}/realms/ApiTest/.well-known/openid-configuration";
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = false,
                    NameClaimType = $"{config["Keycloak:NameClaim"]}"
                };
            });

        builder.Services.AddTransient(services =>
        {
            var service = services.GetService<IHttpContextAccessor>()?.HttpContext?.User;

            return service ?? throw new Exception("Service not found");
        });
    }
}
