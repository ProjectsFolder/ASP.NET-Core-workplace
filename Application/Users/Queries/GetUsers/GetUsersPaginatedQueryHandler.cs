using Application.Interfaces;
using Application.Specifications;
using Application.Users.Queries.Common.Dto;
using Application.Users.Queries.GetUsers.Dto;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Users.Queries.GetUsers;

public class GetUsersPaginatedQueryHandler(
    IRepository<User> repository,
    IMapper mapper)
    : IRequestHandler<GetUsersPaginatedQuery, PaginationDto<UserDto>>
{
    public async Task<PaginationDto<UserDto>> Handle(GetUsersPaginatedQuery request, CancellationToken cancellationToken)
    {
        var total = await repository.CountAsync(cancellationToken);
        var specification = new PaginatedSpecification<User>(request.Page, request.PerPage, total, out int totalPages);
        var users = await repository.ListAsync(specification, cancellationToken);

        var result = new PaginationDto<UserDto>
        {
            Items = users.Select(mapper.Map<UserDto>).ToList(),
            TotalPages = totalPages,
        };

        return result;
    }
}
