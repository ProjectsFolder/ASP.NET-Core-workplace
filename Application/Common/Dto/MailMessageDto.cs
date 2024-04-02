namespace Application.Common.Dto;

public class MailMessageDto
{
    public required string Subject { get; set; }

    public required string Content { get; set; }

    public required string ReceiverAddress { get; set; }

    public string? ReceiverName { get; set; }
}
