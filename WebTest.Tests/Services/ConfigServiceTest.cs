using Microsoft.Extensions.DependencyInjection;
using WebTest.Services;

namespace WebTest.Tests.Services
{
    public class ConfigServiceTest(TestWebApplicationFactory<Program> factory) : BaseTest(factory)
    {
        [Fact]
        public void GetConfig()
        {
            var service = GetService<ConfigService>();
            var value = service?.Get<string>("ApiToken");

            Assert.Equal("test", value);
        }

        [Fact]
        public void GetSection()
        {
            var service = GetService<ConfigService>();
            var value = service?.GetSection("ConnectionStrings", "WebApiDatabase");

            Assert.Equal("Data Source=test.db", value);
        }
    }
}
