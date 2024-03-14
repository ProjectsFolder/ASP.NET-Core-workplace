using WebTest.Attributes;

namespace WebTest.Services
{
    [Service(type: ServiceType.Singleton)]
    public sealed class ContainerService(IServiceProvider serviceProvider)
    {
        public T? GetService<T>(bool scoped = true)
        {
            var provider = scoped ? serviceProvider.CreateScope().ServiceProvider : serviceProvider;
            var service = provider.GetService<T>();

            return service;
        }

        public T GetRequiredService<T>(bool scoped = true)
        {
            var provider = scoped ? serviceProvider.CreateScope().ServiceProvider : serviceProvider;
            var service = provider.GetService<T>() ?? throw new Exception($"Service \"{typeof(T)}\" not found.");

            return service;
        }

        public object? GetService(Type type, bool scoped = true)
        {
            var provider = scoped ? serviceProvider.CreateScope().ServiceProvider : serviceProvider;
            var service = provider.GetService(type);

            return service;
        }

        public object GetRequiredService(Type type, bool scoped = true)
        {
            var provider = scoped ? serviceProvider.CreateScope().ServiceProvider : serviceProvider;
            var service = provider.GetService(type) ?? throw new Exception($"Service \"{type.Name}\" not found.");;

            return service;
        }
    }
}
