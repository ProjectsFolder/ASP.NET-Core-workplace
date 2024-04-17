using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Keycloak;

public static class Dependency
{
    public static IServiceCollection AddKeycloak(this IServiceCollection services)
    {
        services.AddSingleton<IOpenApiAuth, KeycloakClient>();

        return services;
    }
}
