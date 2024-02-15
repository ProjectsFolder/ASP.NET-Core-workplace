using WebTest.Exeptions;
using AspProblemDetailsFactory = Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory;
using ProblemDetailsFactory = WebTest.Exeptions.ProblemDetailsFactory;
using WebTest.Security.Authentication.Token;
using Microsoft.EntityFrameworkCore;
using WebTest.Services;
using System.Reflection;
using WebTest.Transformers;

namespace WebTest.Boot.Register
{
    public static class RegisterAppServices
    {
        public static void AddAppServices(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            builder.Services.AddTransient<AspProblemDetailsFactory, ProblemDetailsFactory>();

            builder.Services.AddControllers();

            builder.Services.AddExceptionHandler<ExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddAuthentication().AddScheme<TokenAuthOptions, TokenAuthHandler>(TokenAuthDefaults.SchemaName, options =>
            {
                options.HeaderName = config.GetValue<string>("ApiTokenHeaderName") ?? "Authorization";
                options.Token = config.GetValue<string>("ApiToken");
            });

            builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(config.GetConnectionString("WebApiDatabase")), ServiceLifetime.Scoped);

            var assembly = Assembly.GetExecutingAssembly();
            var repositoryTypes = assembly.GetTypes()
                .Where(type => type.BaseType == typeof(BaseRepository))
                .DistinctBy(e => e.GetType().Name)
                .ToList();
            foreach (var repositoryType in repositoryTypes)
            {
                builder.Services.AddScoped(repositoryType, services =>
                {
                    var repository = Activator.CreateInstance(repositoryType);
                    if (repository is BaseRepository currentRepository)
                    {
                        var dbContext = services.GetService<DataContext>();
                        if (dbContext != null)
                        {
                            currentRepository.AddContext(dbContext);

                            return currentRepository;
                        }
                    }

                    throw new Exception("Repository injection error");
                });
            }

            var tramsformerTypes = assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Where(e => e.IsGenericType)
                    .Select(e => e.GetGenericTypeDefinition())
                    .Contains(typeof(ITransformer<,>))
                )
                .DistinctBy(e => e.GetType().Name)
                .ToList();
            foreach (var tramsformerType in tramsformerTypes)
            {
                builder.Services.AddScoped(tramsformerType);
            }
        }
    }
}
