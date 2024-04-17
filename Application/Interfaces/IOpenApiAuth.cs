using Application.Common.Dto;

namespace Application.Interfaces;

public interface IOpenApiAuth
{
    Task<OpenApiTokenDto?> GetTokenAsync(string login, string password, CancellationToken cancellationToken);

    Task<OpenApiTokenDto?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
}
