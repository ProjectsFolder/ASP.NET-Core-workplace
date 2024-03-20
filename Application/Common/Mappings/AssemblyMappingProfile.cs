using AutoMapper;
using System.Reflection;

namespace Application.Common.Mappings;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly) => ApplyMappingsFromAssembly(assembly);

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(type => !type.IsAbstract && type.GetInterfaces().Any(i => i == typeof(IMapping)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type) as IMapping;
            instance?.Mapping(this);
        }
    }
}
