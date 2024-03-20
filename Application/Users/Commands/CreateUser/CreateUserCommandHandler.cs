using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Users.Commands.CreateUser;

internal class CreateUserCommandHandler(IRepository<User> repository) : IRequestHandler<CreateUserCommand, int>
{

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Login = request.Login,
            Password = request.Password,
        };

        var model = await repository.AddAsync(user, cancellationToken);

        return model.Id;
    }
}
