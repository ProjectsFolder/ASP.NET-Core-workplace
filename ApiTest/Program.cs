using Api.Build.Authentication;
using Api.Build.Documentation;
using Api.GrpcControllers;
using Api.Middleware;
using Application;
using Application.Common.Mappings;
using Application.Extensions;
using Application.Interfaces;
using Cron;
using EventBus;
using Infrastructure.Cache;
using Infrastructure.Cron;
using Infrastructure.Data;
using Infrastructure.EventBus.Kafka;
using Infrastructure.EventBus.Rabbit;
using Infrastructure.Keycloak;
using Infrastructure.Mail;
using Infrastructure.Template;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddApplication();
builder.Services.EnableAutowiring(Assembly.GetExecutingAssembly());
builder.Services.EnableAutowiring(typeof(IRepository<>).Assembly);
builder.Services.AddIntegrationEvents();
builder.Services.AddCache(config.GetConnectionString("RedisConnection") ?? "");
builder.Services.AddCronParser();
builder.Services.AddDatabase(config.GetConnectionString("DbConnection") ?? "");
builder.Services.AddRabbitMq(config);
builder.Services.AddKafka();
builder.Services.AddKeycloak();
builder.Services.AddMail(config);
builder.Services.AddTemplateProcessor();
builder.Services.AddCronJobs();
builder.Services.AddControllers();
builder.Services.AddApiVersioning()
    .AddApiExplorer(options =>
    {
        options.SubstituteApiVersionInUrl = true;
        options.GroupNameFormat = "'v'VVV";
        options.AssumeDefaultVersionWhenUnspecified = true;
    });
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IRepository<>).Assembly));
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddGrpc();

builder.AddAuthentication();
builder.AddDocumentation();

var app = builder.Build();
app.Services.DatabaseMigrate();
app.Services.DatabaseSeed();
app.MapControllers();
app.MapGrpcService<UserController>();

if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionMiddleware>();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName;
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.Run();

public partial class Program { }
