using Application.Common.Exceptions;
using Application.Domains.Users.Queries.GetUser.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Domains.Users.Queries.GetUser;

public class GetUserByIdHandler(
    IRepository<User> repository,
    IMapper mapper,
    ICache cache) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await cache.GetOrSet(
            $"users::{request.Id}",
            () => Process(request, cancellationToken),
            60,
            cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.Id);

        return result;
    }

    private async Task<UserDto> Process(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.Id);
        var result = mapper.Map<UserDto>(user);

        return result;
    }
}
