using Application.Common.Dto;
using Application.Domains.Users.Queries.GetUsers.Dto;
using MediatR;

namespace Application.Domains.Users.Queries.GetUsers;

public class GetUsersPaginatedQuery : IRequest<PaginationDto<UserDto>>
{
    public required int Page { get; set; }

    public required int PerPage { get; set; }
}
