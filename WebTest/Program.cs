using Microsoft.AspNetCore.Mvc;
using WebTest.Boot.Register;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);
builder.AddAppServices();
builder.AddTimeServices();
builder.AddUserServices();
builder.AddAuthServices();
var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseExceptionHandler();
app.UseAuthorization();

app.MapGet("/", () => "App started!");

app.Run();
