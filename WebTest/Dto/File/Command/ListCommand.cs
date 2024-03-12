namespace WebTest.Dto.File.Command
{
    public class ListCommand : CommandBase
    {
        public int Page { get; set; } = 0;

        public int PerPage { get; set; } = 10;
    }
}
