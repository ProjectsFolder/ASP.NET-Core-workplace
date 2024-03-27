using Application.Common.Mappings;
using Domain;

namespace Application.Domains.Users.Queries.GetUsers.Dto;

public class UserDto : BaseMappingFrom<User>
{
    public required int Id { get; set; }

    public required string Login { get; set; }

    public string? Email { get; set; }
}
