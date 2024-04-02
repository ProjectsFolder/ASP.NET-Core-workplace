using Application.Common.Dto;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Infrastructure.Mail;

public class MailService(SmtpClient smtpClient, IConfiguration configuration) : IMailer
{
    private readonly string fromAddress = configuration["Mail:FromAddress"] ?? "test@mail.com";
    private readonly string fromName = configuration["Mail:FromName"] ?? "test";

    public Task SendAsync(MailMessageDto message, CancellationToken? cancellationToken = null)
    {
        return Task.Run(() =>
        {
            var mail = new MailMessage(
                new MailAddress(fromAddress, fromName),
                new MailAddress(message.ReceiverAddress, message.ReceiverName))
            {
                Subject = message.Subject,
                Body = message.Content,
                IsBodyHtml = true,
            };

            smtpClient.Send(mail);
        }, cancellationToken ?? CancellationToken.None);
    }
}
