namespace WebTest.Dto.File.Command
{
    public class CreateCommand : CommandBase
    {
        public IFormFile File { get; set; } = null!;
    }
}
