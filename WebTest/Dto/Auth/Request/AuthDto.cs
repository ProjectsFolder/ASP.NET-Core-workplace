using System.ComponentModel.DataAnnotations;

namespace WebTest.Dto.Auth.Request
{
    public class AuthDto(string login, string password)
    {
        [Required]
        public string Login { get; set; } = login;

        [Required]
        public string Password { get; set; } = password;
    }
}
