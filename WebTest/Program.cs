using Microsoft.AspNetCore.Mvc;
using WebTest.Boot.Configure;
using WebTest.Boot.Register;
using WebTest.Jobs;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);
builder.AddAppServices();
builder.AddTimeServices();
builder.AddUserServices();
builder.AddAuthServices();
builder.AddCronJob<TestJob>("* * * * *");
var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseExceptionHandler();
app.UseAuthorization();
app.DatabaseMigrate();

app.MapGet("/", () => "App started!");

app.Run();

public partial class Program { }
