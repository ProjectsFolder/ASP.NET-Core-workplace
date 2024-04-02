using Application.Interfaces;
using Application.Services.Mail.Dto.Template;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Template;

public static class Dependency
{
    public static IServiceCollection AddTemplateProcessor(this IServiceCollection services)
    {
        services.AddSingleton<IRender, TemplateService>();
        var types = typeof(ITemplateDto).Assembly.GetExportedTypes()
            .Where(type => !type.IsAbstract && type.GetInterfaces().Any(i => i == typeof(ITemplateDto)))
            .ToList();

        foreach (var type in types)
        {
            DotLiquid.Template.RegisterSafeType(type, type.GetProperties()
                .Select(p => p.Name).ToArray());
        }

        return services;
    }
}
