using Application.Common.Mappings;
using Application.Users.Commands.CreateUser;
using System.ComponentModel.DataAnnotations;

namespace Api.Requests.User;

public class CreateUserRequest : BaseMappingTo<CreateUserCommand>
{
    [Required]
    public required string Login { get; set; }

    [Required]
    public required string Password { get; set; }
}
