namespace WebTest.Dto.OrgStructure.Command
{
    public class CreateCommand : CommandBase
    {
        public required string Login { get; set; }

        public required string Password { get; set; }
    }
}
