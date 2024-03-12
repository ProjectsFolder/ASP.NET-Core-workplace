using WebTest.Domains.Interfaces;
using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.OrgStructure.Command;
using WebTest.Dto.OrgStructure.Response;
using WebTest.Exeptions.Concrete;
using WebTest.Http.Responses;
using WebTest.Http.Transformers;
using WebTest.Models.OrgStructure;
using WebTest.Services;
using WebTest.Transformers.User;

namespace WebTest.Domains.OrgStructure.Handlers
{
    public class UpdateUser(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IRequestResponseHandler<UpdateCommand, UserDto>
    {
        public SuccessDto<UserDto> Handle(UpdateCommand dto)
        {
            var user = userRepository.GetById<User>(dto.Id) ?? throw new ApiException("User not found", 404);
            user.Login = dto.Login;
            user.Password = AuthService.HashPassword(user, dto.Password);
            userRepository.Save(user);

            return SuccessResponseTransformer.Build(user, transformer);
        }
    }
}
