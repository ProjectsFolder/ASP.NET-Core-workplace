using System.Net;
using System.Net.Mail;
using WebTest.Attributes;
using WebTest.Services.Mail.Dto;

namespace WebTest.Services.Mail
{
    [Service(type: ServiceType.Singleton)]
    public sealed class MailService(SmtpClient smtpClient, ConfigService config)
    {
        private readonly SmtpClient _client = smtpClient;
        private readonly string _fromAddress = config.GetSection("Mail", "FromAddress") ?? "test@mail.com";
        private readonly string? _fromName = config.GetSection("Mail", "FromName");

        public void Send(Message message, bool async = true)
        {
            if (async)
            {
                Task.Factory.StartNew(() => Process(message));
            }
            else
            {
                Process(message);
            }
        }

        private void Process(Message message)
        {
            var mail = new MailMessage(
                new MailAddress(_fromAddress, _fromName),
                new MailAddress(message.ReceiverAddress, message.ReceiverName))
            {
                Subject = message.Subject,
                Body = message.Content
            };

            _client.Send(mail);
        }
    }
}
