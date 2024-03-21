using Domain;

namespace Application.Interfaces;

public interface ICurrentUser
{
    User? GetCurrentUser();
}
