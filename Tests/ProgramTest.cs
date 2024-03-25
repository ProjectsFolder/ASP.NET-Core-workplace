using Domain;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

[TestClass]
public class ProgramTest
{
    private static WebApplicationFactory<Program> application = null!;

    public static HttpClient NewClient
    {
        get
        {
            return application.CreateClient();
        }
    }

    public static DatabaseContext DatabaseContext
    {
        get
        {
            return application.Services
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<DatabaseContext>();
        }
    }

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext _)
    {
        application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
            });
    }
}
