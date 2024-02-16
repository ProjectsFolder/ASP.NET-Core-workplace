using WebTest.Attributes;
using WebTest.Domains.User.Repositories;
using WebTest.Dto.User.Response;
using WebTest.Transformers.User;

namespace WebTest.Domains.User.Handlers
{
    [Dependency]
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
