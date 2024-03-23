using Application.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions;

public static class ServiceExtensions
{
    public static void EnableAutowiring(this IServiceCollection services, Assembly assembly)
    {
        var dependencyTypes = assembly.GetTypes()
            .Where(type => type.GetCustomAttributes()
                .Select(e => e.GetType())
                .Contains(typeof(DependencyAttribute))
            )
            .ToList();
        foreach (var dependencyType in dependencyTypes)
        {
            var attr = dependencyType.GetCustomAttribute<DependencyAttribute>();
            if (attr == null)
            {
                continue;
            }

            if (attr.Type == ServiceType.Transient)
            {
                if (attr.BaseType != null)
                {
                    services.AddTransient(attr.BaseType, dependencyType);
                }
                else
                {
                    services.AddTransient(dependencyType);
                }
            }
            else if (attr.Type == ServiceType.Scoped)
            {
                if (attr.BaseType != null)
                {
                    services.AddScoped(attr.BaseType, dependencyType);
                }
                else
                {
                    services.AddScoped(dependencyType);
                }
            }
            else
            {
                if (attr.BaseType != null)
                {
                    services.AddSingleton(attr.BaseType, dependencyType);
                }
                else
                {
                    services.AddSingleton(dependencyType);
                }
            }
        }
    }
}
