using Serilog;
using WebTest.Boot.Configure;
using WebTest.Boot.Register;
using WebTest.Jobs;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    builder.AddSeriolog();
    builder.AddAppServices();
    builder.AddTimeServices();
    builder.AddExceptionServices();
    builder.AddDbServices();
    builder.AddLocalServices();
    builder.AddSmtpClient();
    builder.AddSwagger();
    builder.AddCronJob<DeleteExpiredTokens>("* * * * *");
    var app = builder.Build();

    //app.RequestEnableBuffering();
    app.UseSerilogRequestLogging();
    app.UseRouting();
    app.UseCors(configure =>
    {
        configure.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
    app.MapControllers();
    app.UseExceptionHandler();
    app.UseAuthorization();
    app.DatabaseMigrate();
    app.GenerateSwagger();

    if (app.Environment.IsDevelopment())
    {
        app.MapGet("/", () => "App started!");
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
