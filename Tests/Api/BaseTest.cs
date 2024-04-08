using Application.Utils;
using Domain;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Api;

[TestClass]
public abstract class BaseTest
{
    private WebApplicationFactory<Program> application = null!;

    protected DatabaseContext Database { get; private set; } = null!;

    protected HttpClient NewClient
    {
        get
        {
            return CreateClient().Result;
        }
    }

    protected IServiceProvider ServiceProvider
    {
        get
        {
            return application.Services;
        }
    }

    protected async Task<HttpClient> CreateClient(Domain.User? user = null)
    {
        var options = new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false };
        var client = application.CreateClient(options);
        if (user == null)
        {
            return client;
        }

        var token = await AuthorizedAs(user);
        if (token != null)
        {
            var config = ServiceProvider.GetRequiredService<IConfiguration>();
            client.DefaultRequestHeaders.Add(
                config.GetValue<string>("UserTokenHeaderName") ?? "Authorization",
                $"Bearer {token}");
        }

        return client;
    }

    private async Task<string> AuthorizedAs(Domain.User user)
    {
        if (!await Database.Users.AnyAsync(u => u.Id == user.Id))
        {
            await Database.Users.AddAsync(user);
            await Database.SaveChangesAsync();
        }

        var token = new Token()
        {
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            Value = StringUtils.RandomString(64),
        };
        await Database.Tokens.AddAsync(token);
        await Database.SaveChangesAsync();

        return token.Value;
    }

    [TestInitialize]
    public void TestInitialize()
    {
        application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    var connection = new SqliteConnection("Filename=:memory:");
                    connection.Open();
                    services.AddDbContext<DatabaseContext>(
                        options => options.UseSqlite(connection));
                });
            });

        Database = ServiceProvider.GetRequiredService<DatabaseContext>();
    }
}
