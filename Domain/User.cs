namespace Domain;

public class User : BaseModel
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Email { get; set; } = null;
}
