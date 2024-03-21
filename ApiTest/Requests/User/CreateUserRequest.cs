using Application.Common.Mappings;
using Application.Domains.Users.Commands.CreateUser;
using System.ComponentModel.DataAnnotations;

namespace Api.Requests.User;

public class CreateUserRequest : BaseMappingTo<CreateUserCommand>
{
    [Required]
    public required string Login { get; set; }

    [Required]
    public required string Password { get; set; }

    public string? Email {  get; set; }
}
