using WebTest.Attributes;
using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.User.Request;
using WebTest.Dto.User.Response;
using WebTest.Exeptions.Concrete;
using WebTest.Services;
using WebTest.Transformers.User;

namespace WebTest.Domains.OrgStructure.Handlers
{
    [Service]
    public class UpdateUser(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IHandler<UpdateDto, UserDto>
    {
        public UserDto? Handle(UpdateDto? dto)
        {
            if (dto == null)
            {
                return null;
            }

            var user = userRepository.GetUser(dto.Id) ?? throw new ApiException("User not found", 404);
            user.Login = dto.Login;
            user.Password = AuthService.HashPassword(user, dto.Password);
            userRepository.Save(user);

            return transformer.Transform(user);
        }
    }
}
