using System.ComponentModel.DataAnnotations.Schema;
using WebTest.Models.OrgStructure;

namespace WebTest.Models.Auth
{
    [Table("tokens")]
    public class Token : ModelBase
    {
        [Column("token")]
        public string Value { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
