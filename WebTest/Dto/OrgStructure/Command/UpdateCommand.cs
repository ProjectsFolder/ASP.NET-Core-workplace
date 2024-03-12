namespace WebTest.Dto.OrgStructure.Command
{
    public class UpdateCommand : CommandBase
    {
        public int Id { get; set; }

        public required string Login { get; set; }

        public required string Password { get; set; }
    }
}
