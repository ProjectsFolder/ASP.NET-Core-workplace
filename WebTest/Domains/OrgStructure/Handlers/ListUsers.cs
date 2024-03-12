using WebTest.Domains.Interfaces;
using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.OrgStructure.Response;
using WebTest.Http.Responses;
using WebTest.Http.Transformers;
using WebTest.Transformers.User;

namespace WebTest.Domains.OrgStructure.Handlers
{
    public class ListUsers(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IResponseHandler<UserDto>
    {
        public SuccessDto<UserDto> Handle()
        {
            var users = userRepository.GetUsers();

            return SuccessResponseTransformer.Build(users, transformer);
        }
    }
}
