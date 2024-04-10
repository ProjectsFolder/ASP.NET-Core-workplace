using Application.Domains.Users.Queries.GetUser.Dto;
using MediatR;

namespace Application.Domains.Users.Queries.GetUser;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public int Id { get; set; }
}
