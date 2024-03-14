using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using WebTest.Services.Database;

namespace WebTest.Boot.Configure
{
    public static class ConfigureAppServices
    {
        private static readonly object locker = new();

        public static void DatabaseMigrate(this WebApplication application)
        {
            lock (locker)
            {
                using var scope = application.Services.CreateScope();
                var context = scope.ServiceProvider.GetService<DatabaseContext>();
                context?.Database.Migrate();
            }
        }

        public static void RequestEnableBuffering(this WebApplication application)
        {
            application.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });
        }

        public static void GenerateSwagger(this WebApplication application)
        {
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }
            // var host = application.Configuration.GetValue<string>("AppHost");
            // var swagger = application.Services.GetService<ISwaggerProvider>();
            // var doc = swagger?.GetSwagger("v1", host, "/");
            // var swaggerFile = doc.SerializeAsJson(Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0);
            // File.WriteAllText("openapi.json", swaggerFile);
        }
    }
}
