namespace WebTest.Dto.Auth.Command
{
    public class AuthCommand(string login, string password)
    {
        public string Login { get; set; } = login;

        public string Password { get; set; } = password;
    }
}
