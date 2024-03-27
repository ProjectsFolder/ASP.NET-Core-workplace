using Application.Common.Mappings;
using Application.Domains.Users.Queries.GetUsers.Dto;
using System.Text.Json.Serialization;

namespace Api.Responses.User;

public class UserResponse : BaseMappingFrom<UserDto>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public required int Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public required string Login { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Email { get; set; }
}
