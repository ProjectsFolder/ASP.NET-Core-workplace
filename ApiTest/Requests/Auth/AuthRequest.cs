using Application.Common.Mappings;
using Application.Domains.Auth.Commands.KeycloakLogin;
using Application.Domains.Auth.Commands.Login;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Api.Requests.Auth;

public class AuthRequest : IMapping
{
    [Required]
    public required string Login { get; set; }

    [Required]
    public required string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap(GetType(), typeof(LoginCommand));
        profile.CreateMap(GetType(), typeof(KeycloakLoginCommand));
    }
}
