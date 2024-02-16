using WebTest.Exeptions;
using AspProblemDetailsFactory = Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory;
using ProblemDetailsFactory = WebTest.Exeptions.ProblemDetailsFactory;
using Microsoft.EntityFrameworkCore;
using WebTest.Services;
using System.Reflection;
using WebTest.Transformers;
using WebTest.Security.Authentication.UserToken;
using WebTest.Security.Authentication.ApiToken;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using WebTest.Attributes;

namespace WebTest.Boot.Register
{
    public static class RegisterAppServices
    {
        public static void AddAppServices(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<AspProblemDetailsFactory, ProblemDetailsFactory>();
            builder.Services.AddTransient(services => {
                var service = services.GetService<IHttpContextAccessor>()?.HttpContext?.User;
                return service ?? throw new Exception("Service not found");
            });
            builder.Services.AddTransient<AuthService>();

            builder.Services.AddControllers();

            builder.Services.AddExceptionHandler<ExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddAuthentication().AddScheme<ApiTokenOptions, ApiTokenHandler>(ApiTokenDefaults.SchemaName, options =>
            {
                options.HeaderName = config.GetValue<string>("ApiTokenHeaderName") ?? "Authorization";
                options.Token = config.GetValue<string>("ApiToken");
            });
            builder.Services.AddAuthentication().AddScheme<UserTokenOptions, UserTokenHandler>(UserTokenDefaults.SchemaName, options =>
            {
                options.HeaderName = config.GetValue<string>("UserTokenHeaderName") ?? "Authorization";
            });

            builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(config.GetConnectionString("WebApiDatabase")), ServiceLifetime.Scoped);

            var assembly = Assembly.GetExecutingAssembly();
            var repositoryTypes = assembly.GetTypes()
                .Where(type => type.BaseType == typeof(BaseRepository))
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

            var transformerTypes = assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Where(e => e.IsGenericType)
                    .Select(e => e.GetGenericTypeDefinition())
                    .Contains(typeof(ITransformer<,>))
                )
                .ToList();
            foreach (var transformerType in transformerTypes)
            {
                builder.Services.AddScoped(transformerType);
            }

            var dependencyTypes = assembly.GetTypes()
                .Where(type => type.GetCustomAttributes()
                    .Select(e => e.GetType())
                    .Contains(typeof(Dependency))
                )
                .ToList();
            foreach (var dependencyType in dependencyTypes)
            {
                builder.Services.AddScoped(dependencyType);
            }
        }
    }
}
