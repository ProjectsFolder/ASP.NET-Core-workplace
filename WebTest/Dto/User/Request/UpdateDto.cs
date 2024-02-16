using System.ComponentModel.DataAnnotations;

namespace WebTest.Dto.User.Request
{
    public class UpdateDto
    {
        public int Id { get; set; }

        [Required]
        public required string Login { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
