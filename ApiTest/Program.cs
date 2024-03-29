using Api.Build.Authentication;
using Api.Build.Documentation;
using Api.Middleware;
using Application;
using Application.Common.Mappings;
using Application.Extensions;
using Application.Interfaces;
using Cron;
using EventBus;
using Infrastructure;
using Infrastructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddApplication();
builder.Services.EnableAutowiring(Assembly.GetExecutingAssembly());
builder.Services.EnableAutowiring(typeof(IRepository<>).Assembly);
builder.Services.AddIntegrationEvents();
builder.Services.AddDatabase(config.GetConnectionString("DbConnection") ?? "");
builder.Services.AddRabbitMq();
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

builder.AddAuthentication();
builder.AddDocumentation();

var app = builder.Build();
app.Services.DatabaseMigrate();
app.Services.DatabaseSeed();
app.MapControllers();

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
