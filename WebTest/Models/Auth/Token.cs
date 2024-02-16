using System.ComponentModel.DataAnnotations.Schema;

namespace WebTest.Models.Auth
{
    public class Token : BaseModel
    {
        [Column("token")]
        public string Value { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; } = null!;

        public string GetId()
        {
            return Id.ToString();
        }
    }
}
