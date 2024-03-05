using System.ComponentModel.DataAnnotations;

namespace WebTest.Dto.File.Request
{
    public class CreateDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
