namespace WebTest.Models.OrgStructure
{
    public class User : BaseModel
    {
        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string GetId()
        {
            return Id.ToString();
        }
    }
}
