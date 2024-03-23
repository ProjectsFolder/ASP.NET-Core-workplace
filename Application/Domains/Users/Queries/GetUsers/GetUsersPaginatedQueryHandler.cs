using Application.Common.Dto;
using Application.Domains.Users.Queries.GetUsers.Dto;
using Application.Interfaces;
using Application.Specifications;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Domains.Users.Queries.GetUsers;

public class GetUsersPaginatedQueryHandler(
    IRepository<User> repository,
    IMapper mapper) : IRequestHandler<GetUsersPaginatedQuery, PaginationDto<UserDto>>
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
