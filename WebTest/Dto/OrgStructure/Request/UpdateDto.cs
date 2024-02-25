using System.ComponentModel.DataAnnotations;
using WebTest.Attributes.Validation;
using WebTest.Models.OrgStructure;

namespace WebTest.Dto.OrgStructure.Request
{
    public class UpdateDto
    {
        public int Id { get; set; }

        [Required]
        [Unique<User>("login", "id")]
        public required string Login { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
