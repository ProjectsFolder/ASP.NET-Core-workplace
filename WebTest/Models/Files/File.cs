using System.ComponentModel.DataAnnotations.Schema;
using WebTest.Models.OrgStructure;

namespace WebTest.Models.Files
{
    [Table("files")]
    public class UserFile : BaseModel
    {
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("path")]
        public string Path { get; set; } = string.Empty;

        [Column("content_type")]
        public string ContentType { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
