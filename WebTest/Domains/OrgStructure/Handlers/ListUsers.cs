using WebTest.Attributes;
using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.User.Response;
using WebTest.Transformers.User;

namespace WebTest.Domains.OrgStructure.Handlers
{
    [Service]
    public class ListUsers(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IHandler<object, IEnumerable<UserDto>>
    {
        public IEnumerable<UserDto>? Handle(object? dto)
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
