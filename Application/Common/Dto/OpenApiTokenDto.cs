using System.Text.Json.Serialization;

namespace Application.Common.Dto;

public class OpenApiTokenDto
{
    public required string AccessToken { get; set; }

    public required string RefreshToken { get; set; }

    public int ExpiresIn { get; set; }

    public int RefreshExpiresIn { get; set; }
}
