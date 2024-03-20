using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class Dependency
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
