using Application.Users.Queries.Common.Dto;
using Application.Users.Queries.GetUsers.Dto;
using MediatR;

namespace Application.Users.Queries.GetUsers;

public class GetUsersPaginatedQuery : IRequest<PaginationDto<UserDto>>
{
    public required int Page {  get; set; }

    public required int PerPage {  get; set; }
}
