using WebTest.Attributes;
using WebTest.Domains.Auth.Repositories;
using WebTest.Dto.Auth.Request;
using WebTest.Dto.Auth.Response;
using WebTest.Exeptions.Concrete;
using WebTest.Models.Auth;
using WebTest.Services;
using WebTest.Utils;

namespace WebTest.Domains.Auth.Handlers
{
    [Service]
    public class Login(
        DataContext context,
        TokenRepository tokenRepository,
        UserRepository userRepository
        ) : IHandler<AuthDto, TokenDto>
    {
        public TokenDto? Handle(AuthDto? dto)
        {
            if (dto == null)
            {
                return null;
            }

            return context.Transaction(() =>
            {
                return Process(dto);
            });
        }

        private TokenDto? Process(AuthDto dto)
        {
            var user = userRepository.GetUserByLogin(dto.Login) ?? throw new ApiException("User not found", 404);
            if (!AuthService.CheckPassword(user, dto.Password))
            {
                throw new ApiException("Incorrect password", 403);
            }

            tokenRepository.DeleteAllByUser(user.Id);

            var token = new Token()
            {
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
                Value = StringUtils.RandomString(64)
            };

            tokenRepository.Save(token);

            return new TokenDto() { Token = token.Value };
        }
    }
}
