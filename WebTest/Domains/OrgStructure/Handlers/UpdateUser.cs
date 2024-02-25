using WebTest.Domains.Interfaces;
using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.OrgStructure.Request;
using WebTest.Dto.OrgStructure.Response;
using WebTest.Exeptions.Concrete;
using WebTest.Services;
using WebTest.Transformers.User;

namespace WebTest.Domains.OrgStructure.Handlers
{
    public class UpdateUser(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IRequestResponseHandler<UpdateDto, UserDto>
    {
        public UserDto Handle(UpdateDto dto)
        {
            var user = userRepository.GetUser(dto.Id) ?? throw new ApiException("User not found", 404);
            user.Login = dto.Login;
            user.Password = AuthService.HashPassword(user, dto.Password);
            userRepository.Save(user);

            return transformer.Transform(user);
        }
    }
}
