namespace Domain
{
    public class Token : BaseModel
    {
        public string Value { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
