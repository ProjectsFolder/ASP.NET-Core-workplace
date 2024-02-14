using WebTest.Services.Interfaces;
using WebTest.Services;
using WebTest.Domains.Time;
using WebTest.Exeptions;
using AspProblemDetailsFactory = Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory;
using ProblemDetailsFactory = WebTest.Exeptions.ProblemDetailsFactory;
using WebTest.Security.Authentication.Token;

namespace WebTest.Boot
{
    public static class ServiceRegister
    {
        public static void RegisterAppServices(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            builder.Services.AddTransient<AspProblemDetailsFactory, ProblemDetailsFactory>();
            builder.Services.AddTransient<ITimeService, ShortTimeService>();
            builder.Services.AddTransient<GetTimeHandler>();

            builder.Services.AddControllers();

            builder.Services.AddExceptionHandler<ExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddAuthentication().AddScheme<TokenAuthOptions, TokenAuthHandler>(TokenAuthDefaults.SchemaName, options => {
                options.HeaderName = config.GetValue<string>("ApiTokenHeaderName") ?? "Authorization";
                options.Token = config.GetValue<string>("ApiToken");
            });
        }
    }
}
