namespace EventBus.Events.MassCreateUsers.Dto;

public class UserDto
{
    public required string Login {  get; set; }

    public required string Password { get; set; }

    public string? Email { get; set; }
}
