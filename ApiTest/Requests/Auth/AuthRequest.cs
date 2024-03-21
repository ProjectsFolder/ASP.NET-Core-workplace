using Application.Common.Mappings;
using Application.Domains.Auth.Commands.Login;
using System.ComponentModel.DataAnnotations;

namespace Api.Requests.Auth
{
    public class AuthRequest : BaseMappingTo<LoginCommand>
    {
        [Required]
        public required string Login { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
