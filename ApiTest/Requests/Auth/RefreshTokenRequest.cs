using Application.Common.Mappings;
using Application.Domains.Auth.Commands.KeycloakRefreshToken;
using System.ComponentModel.DataAnnotations;

namespace Api.Requests.Auth;

public class RefreshTokenRequest : BaseMappingTo<KeycloakRefreshTokenCommand>
{
    [Required]
    public required string RefreshToken { get; set; }
}
