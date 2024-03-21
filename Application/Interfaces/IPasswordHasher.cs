using Domain;

namespace Application.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(User user, string password);

    bool CheckPassword(User user, string password);
}
