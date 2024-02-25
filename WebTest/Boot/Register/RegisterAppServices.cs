using WebTest.Exeptions;
using AspProblemDetailsFactory = Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory;
using ProblemDetailsFactory = WebTest.Exeptions.ProblemDetailsFactory;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebTest.Transformers;
using WebTest.Security.Authentication.UserToken;
using WebTest.Security.Authentication.ApiToken;
using WebTest.Attributes;
using WebTest.Jobs;
using NCrontab;
using WebTest.Services.Jobs;
using WebTest.Services.Database;
using WebTest.Domains.Interfaces;

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

            builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(config.GetConnectionString("WebApiDatabase")));

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
                        var dbContext = services.GetService<DatabaseContext>();
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

            var handlerTypes = assembly.GetTypes()
                .Where(type => type.GetInterfaces().Contains(typeof(IHandler)) && !type.IsInterface)
                .ToList();
            foreach (var handlerType in handlerTypes)
            {
                builder.Services.AddScoped(handlerType);
            }

            var dependencyTypes = assembly.GetTypes()
                .Where(type => type.GetCustomAttributes()
                    .Select(e => e.GetType())
                    .Contains(typeof(ServiceAttribute))
                )
                .ToList();
            foreach (var dependencyType in dependencyTypes)
            {
                builder.Services.AddScoped(dependencyType);
            }
        }

        public static void AddCronJob<T>(this WebApplicationBuilder builder, string cronExpression)
            where T : class, ICronJob
        {
            var cron = CrontabSchedule.TryParse(cronExpression)
                       ?? throw new ArgumentException("Invalid cron expression", nameof(cronExpression));

            builder.Services.AddHostedService<CronScheduler>();
            builder.Services.AddSingleton<T>();
            builder.Services.AddSingleton(new CronRegistryEntry(typeof(T), cron));
        }
    }
}
