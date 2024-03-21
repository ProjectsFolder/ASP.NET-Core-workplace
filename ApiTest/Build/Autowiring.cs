using Application.Common.Attributes;
using System.Reflection;

namespace Api.Build
{
    public static class Autowiring
    {
        public static void EnableAutowiring(this WebApplicationBuilder builder, Assembly assembly)
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
                        builder.Services.AddTransient(attr.BaseType, dependencyType);
                    }
                    else
                    {
                        builder.Services.AddTransient(dependencyType);
                    }
                }
                else if (attr.Type == ServiceType.Scoped)
                {
                    if (attr.BaseType != null)
                    {
                        builder.Services.AddScoped(attr.BaseType, dependencyType);
                    }
                    else
                    {
                        builder.Services.AddScoped(dependencyType);
                    }
                }
                else
                {
                    if (attr.BaseType != null)
                    {
                        builder.Services.AddSingleton(attr.BaseType, dependencyType);
                    }
                    else
                    {
                        builder.Services.AddSingleton(dependencyType);
                    }
                }
            }
        }
    }
}
