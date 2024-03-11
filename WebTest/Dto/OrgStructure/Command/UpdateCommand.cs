namespace WebTest.Dto.OrgStructure.Command
{
    public class UpdateCommand
    {
        public int Id { get; set; }

        public required string Login { get; set; }

        public required string Password { get; set; }
    }
}
