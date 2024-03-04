using WebTest.Attributes;

namespace WebTest.Services
{
    [Service(type: ServiceType.Singleton)]
    public sealed class ConfigService(IConfiguration configuration)
    {
        public T? Get<T>(string key, T? defaultValue = default)
        {
            var value = configuration.GetValue(typeof(T), key);

            return value is T result ? result : defaultValue;
        }

        public string? GetSection(string section, string key, string? defaultValue = default)
        {
            return configuration.GetSection(section)[key] ?? defaultValue;
        }
    }
}
