namespace Application.Services.Mail.Dto;

public class MailTemplateDto
{
    public required string Subject { get; set; }

    public required string Template { get; set; }

    public required object TemplateParameters { get; set; }

    public required string ReceiverAddress { get; set; }

    public string? ReceiverName { get; set; }
}
