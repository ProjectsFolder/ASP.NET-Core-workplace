using WebTest.Attributes;

namespace WebTest.Services
{
    [Service]
    public class ConfigService(IConfiguration configuration)
    {
        public T? Get<T>(string key, T? defaultValue = default)
            where T : class
        {
            return configuration.GetValue(typeof(T), key) as T ?? defaultValue;
        }

        public string? GetSection(string section, string key, string? defaultValue = default)
        {
            return configuration.GetSection(section)[key] ?? defaultValue;
        }
    }
}
