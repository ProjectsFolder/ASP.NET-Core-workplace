using Application.Common.Attributes;
using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;

namespace Application.Services;

[Dependency]
public sealed class AuthService(ICurrentUser currentUser)
{
    public User GetCurrentUser()
    {
        return currentUser.GetCurrentUser()
            ?? throw new AuthenticateException("User not authenticated");
    }
}
