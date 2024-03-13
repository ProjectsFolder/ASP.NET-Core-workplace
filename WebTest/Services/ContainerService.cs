using WebTest.Attributes;

namespace WebTest.Services
{
    [Service(type: ServiceType.Singleton)]
    public sealed class ContainerService(IServiceProvider serviceProvider)
    {
        public T? GetService<T>(bool scoped = true, bool throwable = false)
        {
            var provider = scoped ? serviceProvider.CreateScope().ServiceProvider : serviceProvider;
            var service = provider.GetService<T>();

            if (throwable && service == null)
            {
                throw new Exception($"Service \"{typeof(T)}\" not found.");
            }

            return service;
        }
    }
}
