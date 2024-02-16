using WebTest.Domains.User.Repositories;
using WebTest.Dto.User.Request;
using WebTest.Dto.User.Response;
using WebTest.Exeptions.Concrete;
using WebTest.Transformers.User;

namespace WebTest.Domains.User.Handlers
{
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
            user.Password = dto.Password;
            userRepository.Save(user);

            return transformer.Transform(user);
        }
    }
}
