using Api.Build;
using Application;
using Application.Common.Mappings;
using Application.Interfaces;
using Cron;
using Cron.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddApplication();
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

builder.EnableAutowiring(Assembly.GetExecutingAssembly());
builder.EnableAutowiring(typeof(IRepository<>).Assembly);
builder.AddAuthentication();

var app = builder.Build();
app.Services.DatabaseMigrate();
app.MapControllers();

app.Run();
