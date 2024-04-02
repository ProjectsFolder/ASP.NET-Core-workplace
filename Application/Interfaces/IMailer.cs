using Application.Common.Dto;

namespace Application.Interfaces;

public interface IMailer
{
    Task SendAsync(MailMessageDto message, CancellationToken? cancellationToken = null);
}
