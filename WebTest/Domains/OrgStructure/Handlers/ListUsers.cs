using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.User.Response;
using WebTest.Transformers.User;

namespace WebTest.Domains.OrgStructure.Handlers
{
    public class ListUsers(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IResponseHandler<IEnumerable<UserDto>>
    {
        public IEnumerable<UserDto> Handle()
        {
            List<UserDto> response = [];
            var users = userRepository.GetUsers();
            foreach (var user in users)
            {
                response.Add(transformer.Transform(user));
            }

            return response;
        }
    }
}
