using WebTest.Domains.Interfaces;
using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.OrgStructure.Command;
using WebTest.Http.Responses;
using WebTest.Http.Transformers;
using WebTest.Models.OrgStructure;
using WebTest.Services;
using WebTest.Transformers.User;

namespace WebTest.Domains.OrgStructure.Handlers
{
    public class CreateUser(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IRequestResponseHandler<CreateCommand, SuccessDto>
    {
        public SuccessDto Handle(CreateCommand dto)
        {
            var user = new User()
            {
                Login = dto.Login,
            };
            user.Password = AuthService.HashPassword(user, dto.Password);

            userRepository.Save(user);

            return SuccessResponseTransformer.Build(user, transformer);
        }
    }
}
