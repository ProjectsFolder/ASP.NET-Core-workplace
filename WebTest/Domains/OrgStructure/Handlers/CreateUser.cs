using WebTest.Attributes;
using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.User.Request;
using WebTest.Dto.User.Response;
using WebTest.Models.OrgStructure;
using WebTest.Services;
using WebTest.Transformers.User;

namespace WebTest.Domains.OrgStructure.Handlers
{
    [Service]
    public class CreateUser(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IHandler<CreateDto, UserDto>
    {
        public UserDto? Handle(CreateDto? dto)
        {
            if (dto == null)
            {
                return null;
            }

            var user = new User()
            {
                Login = dto.Login,
            };
            user.Password = AuthService.HashPassword(user, dto.Password);

            userRepository.Save(user);

            return transformer.Transform(user);
        }
    }
}
