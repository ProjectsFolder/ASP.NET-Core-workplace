using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WebTest.Models.Auth;
using WebTest.Models.OrgStructure;
using WebTest.Services;
using WebTest.Services.Database;
using WebTest.Utils;

namespace WebTest.Tests
{
    public abstract class BaseTest : IClassFixture<TestWebApplicationFactory<Program>>
    {
        private readonly TestWebApplicationFactory<Program> factory;
        protected IServiceScope scope;
        protected ConfigService configService;
        protected DataContext db;

        public BaseTest(TestWebApplicationFactory<Program> factory)
        {
            this.factory = factory;
            scope = factory.Services.CreateScope();
            var db = GetService<DataContext>();
            this.db = db ?? throw new Exception("Db servcie not found");

            var configService = GetService<ConfigService>();
            this.configService = configService ?? throw new Exception("Config servcie not found");
        }

        protected HttpClient CreateClient(
            string? authToken = default,
            WebApplicationFactoryClientOptions? options = default)
        {
            if (options == null)
            {
                options = new WebApplicationFactoryClientOptions()
                {
                    AllowAutoRedirect = false
                };
            }

            var client = factory.CreateClient(options);
            if (authToken != null)
            {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                client.DefaultRequestHeaders.Add(configService.Get("UserTokenHeaderName", "Authorization"), $"Bearer {authToken}");
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            }

            return client;
        }

        protected string AuthorizedAs(User user)
        {
            db.Users.Add(user);

            db.SaveChanges();

            var token = new Token()
            {
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                Value = StringUtils.RandomString(64),
            };
            db.Tokens.Add(token);

            db.SaveChanges();

            return token.Value;
        }

        protected T? GetService<T>()
            where T : class
        {
            return scope.ServiceProvider.GetService(typeof(T)) as T;
        }

        protected void ReinitializeDatabase()
        {
            db.Tokens.RemoveRange(db.Tokens);
            db.Users.RemoveRange(db.Users);
            Seeding();
            db.SaveChanges();
        }

        private void Seeding()
        {
            var user = new User() { Login = "test" };
            user.Password = AuthService.HashPassword(user, "test");
            db.Users.Add(user);
        }
    }
}
