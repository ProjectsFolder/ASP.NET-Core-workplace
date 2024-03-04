namespace WebTest.Services.Mail.Dto
{
    public class Message(string subject, string content, string receiverAddress, string? receiverName = default)
    {
        public string Subject { get; set; } = subject;

        public string Content { get; set; } = content;

        public string ReceiverAddress { get; set; } = receiverAddress;

        public string? ReceiverName { get; set; } = receiverName;
    }
}
