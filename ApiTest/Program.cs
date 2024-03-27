using Api.Build.Authentication;
using Api.Build.Documentation;
using Api.Middleware;
using Application;
using Application.Common.Mappings;
using Application.Extensions;
using Application.Interfaces;
using Cron;
using Infrastructure;
using Infrastructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddApplication();
builder.Services.EnableAutowiring(Assembly.GetExecutingAssembly());
builder.Services.EnableAutowiring(typeof(IRepository<>).Assembly);
builder.Services.AddInfrastructure(config.GetConnectionString("DbConnection") ?? "");
builder.Services.AddCronJobs();
builder.Services.AddControllers();
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
    app.UseSwaggerUI();
}

app.Run();

public partial class Program { }
