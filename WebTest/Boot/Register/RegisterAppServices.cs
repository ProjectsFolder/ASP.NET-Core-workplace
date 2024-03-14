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
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using WebTest.Boot.Swagger;
using WebTest.Services.Database.Interfaces;

namespace WebTest.Boot.Register
{
    public static class RegisterAppServices
    {
        public static void AddAppServices(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            builder.Services.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient(services => {
                var service = services.GetService<IHttpContextAccessor>()?.HttpContext?.User;
                return service ?? throw new Exception("Service not found");
            });

            builder.Services.AddAuthentication().AddScheme<ApiTokenOptions, ApiTokenHandler>(ApiTokenDefaults.SchemaName, options =>
            {
                options.HeaderName = config.GetValue<string>("ApiTokenHeaderName") ?? "Authorization";
                options.Token = config.GetValue<string>("ApiToken");
            });
            builder.Services.AddAuthentication().AddScheme<UserTokenOptions, UserTokenHandler>(UserTokenDefaults.SchemaName, options =>
            {
                options.HeaderName = config.GetValue<string>("UserTokenHeaderName") ?? "Authorization";
            });

            builder.Services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = long.MaxValue;
            });
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
            });
        }

        public static void AddExceptionServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<AspProblemDetailsFactory, ProblemDetailsFactory>();
            builder.Services.AddExceptionHandler<ExceptionHandler>();
            builder.Services.AddProblemDetails();
        }

        public static void AddDbServices(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(config.GetConnectionString("WebApiDatabase")));

            var assembly = Assembly.GetExecutingAssembly();
            var repositoryTypes = assembly.GetTypes()
                .Where(type => type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(RepositoryBase<>))
                .ToList();
            foreach (var repositoryType in repositoryTypes)
            {
                builder.Services.AddScoped(repositoryType, services =>
                {
                    var repository = Activator.CreateInstance(repositoryType);
                    if (repository is IRepository currentRepository)
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
        }

        public static void AddLocalServices(this WebApplicationBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

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
                var attr = dependencyType.GetCustomAttribute<ServiceAttribute>();
                if (attr == null)
                {
                    continue;
                }

                if (attr.Type == ServiceType.Transient)
                {
                    builder.Services.AddTransient(dependencyType);
                }
                else if (attr.Type == ServiceType.Scoped)
                {
                    builder.Services.AddScoped(dependencyType);
                }
                else
                {
                    builder.Services.AddSingleton(dependencyType);
                }
            }
        }

        public static void AddSmtpClient(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            builder.Services.AddSingleton(services =>
            {
                var host = config.GetSection("Mail")["Host"];
                var username = config.GetSection("Mail")["Username"];
                var password = config.GetSection("Mail")["Password"];
                if (!int.TryParse(config.GetSection("Mail")["Port"], out int port))
                {
                    port = 0;
                }

                return new SmtpClient(host, port)
                {
                    EnableSsl = false,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(username, password)
                };
            });
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebTest API", Version = "v1" });
                options.OperationFilter<PrepareFilter>();
                options.EnableAnnotations();
                options.AddSecurityDefinition(UserTokenDefaults.SchemaName, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = config.GetValue<string>("UserTokenHeaderName") ?? "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Application security",
                });
                options.AddSecurityDefinition(ApiTokenDefaults.SchemaName, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = config.GetValue<string>("ApiTokenHeaderName") ?? "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "External security",
                });
            });
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
