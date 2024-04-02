using Application.Common.Attributes;
using Application.Common.Dto;
using Application.Interfaces;
using Application.Services.Mail.Dto;

namespace Application.Services.Mail;

[Dependency(type: ServiceType.Singleton)]
public sealed class MailService(IMailer mailer, IRender render)
{
    public Task SendAsync(MailTemplateDto message, CancellationToken? cancellationToken = null)
    {
        var resultMessage = new MailMessageDto
        {
            Subject = message.Subject,
            Content = render.Render(message.Template, message.TemplateParameters),
            ReceiverAddress = message.ReceiverAddress,
            ReceiverName = message.ReceiverName,
        };

        return mailer.SendAsync(resultMessage, cancellationToken);
    }
}
