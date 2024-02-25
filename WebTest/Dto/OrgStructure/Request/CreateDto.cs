using System.ComponentModel.DataAnnotations;
using WebTest.Attributes.Validation;
using WebTest.Models.OrgStructure;

namespace WebTest.Dto.OrgStructure.Request
{
    public class CreateDto
    {
        [Required]
        [Unique<User>("login")]
        public required string Login { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
