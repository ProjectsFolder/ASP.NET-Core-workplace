using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Mail;

public static class Dependency
{
    public static IServiceCollection AddMail(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(services =>
        {
            var host = configuration["Mail:Host"];
            var username = configuration["Mail:Username"];
            var password = configuration["Mail:Password"];
            _ = int.TryParse(configuration["Mail:Port"], out int port);

            return new SmtpClient(host, port)
            {
                EnableSsl = false,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };
        });
        services.AddSingleton<IMailer, MailService>();

        return services;
    }
}
