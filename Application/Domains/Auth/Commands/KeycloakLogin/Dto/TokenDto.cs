using Application.Common.Dto;
using Application.Common.Mappings;

namespace Application.Domains.Auth.Commands.KeycloakLogin.Dto;

public class TokenDto : BaseMappingFrom<OpenApiTokenDto>
{
    public required string AccessToken { get; set; }

    public required string RefreshToken { get; set; }

    public int ExpiresIn { get; set; }

    public int RefreshExpiresIn { get; set; }
}
