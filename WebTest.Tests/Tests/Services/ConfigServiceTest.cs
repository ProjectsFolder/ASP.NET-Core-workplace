using WebTest.Services;

namespace WebTest.Tests.Tests.Services
{
    public class ConfigServiceTest(TestWebApplicationFactory<Program> factory) : TestBase(factory)
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

            Assert.Equal("Host=127.0.0.1:16002;Port=5432;Database=webtest_test;Username=postgres;Password=password", value);
        }
    }
}
