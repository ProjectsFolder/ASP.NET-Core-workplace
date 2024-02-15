namespace WebTest.Models
{
    public class User : IModel
    {
        public int? Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? GetId()
        {
            return Id?.ToString();
        }
    }
}
