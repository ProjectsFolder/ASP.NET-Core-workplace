using Microsoft.AspNetCore.Mvc;
using WebTest.Boot;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);
builder.RegisterAppServices();
var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseExceptionHandler();
app.UseAuthorization();

app.MapGet("/", () => "App started!");

app.Run();
