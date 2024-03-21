using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Domains.Users.Commands.CreateUser;

internal class CreateUserCommandHandler(
    IRepository<User> repository,
    ITransaction transaction,
    IPasswordHasher passwordHasher)
    : IRequestHandler<CreateUserCommand, int>
{

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await transaction.ExecuteAsync(() => Process(request, cancellationToken), cancellationToken);
    }

    private async Task<int> Process(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Login = request.Login,
            Password = "new",
            Email = request.Email,
        };

        var model = await repository.AddAsync(user, cancellationToken);

        user.Password = passwordHasher.HashPassword(user, request.Password);

        await repository.UpdateAsync(user, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        return model.Id;
    }
}
