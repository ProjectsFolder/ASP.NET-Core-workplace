using Application.Common.Dto;
using Application.Extensions;
using Application.Interfaces;
using Infrastructure.Keycloak.Dto;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Keycloak;

public class KeycloakClient(IConfiguration config) : IOpenApiAuth
{
    private readonly HttpClient client = new();

    public async Task<OpenApiTokenDto?> GetTokenAsync(string login, string password, CancellationToken cancellationToken)
    {
        var requestContent = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("client_id", "account"),
                new KeyValuePair<string, string>("username", login),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password"),
            ]);
        var response = await client.PostAsync(
            $"{config["Keycloak:ServerUrl"]}/realms/{config["Keycloak:Realm"]}/protocol/openid-connect/token",
            requestContent,
            cancellationToken);

        var str = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode
            && str.TryParseJson(out TokenDto? token)
            && token != null)
        {
            return new OpenApiTokenDto
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                ExpiresIn = token.ExpiresIn,
                RefreshExpiresIn = token.RefreshExpiresIn,
            };
        }

        return null;
    }

    public async Task<OpenApiTokenDto?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var requestContent = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("client_id", "account"),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
            ]);
        var response = await client.PostAsync(
            $"{config["Keycloak:ServerUrl"]}/realms/{config["Keycloak:Realm"]}/protocol/openid-connect/token",
            requestContent,
            cancellationToken);

        var str = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode
            && str.TryParseJson(out TokenDto? token)
            && token != null)
        {
            return new OpenApiTokenDto
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                ExpiresIn = token.ExpiresIn,
                RefreshExpiresIn = token.RefreshExpiresIn,
            };
        }

        return null;
    }
}
