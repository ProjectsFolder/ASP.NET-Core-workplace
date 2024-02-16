namespace WebTest.Models.User
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
