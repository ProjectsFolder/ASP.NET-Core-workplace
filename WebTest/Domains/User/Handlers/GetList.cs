using WebTest.Domains.User.Repositories;
using WebTest.Dto.User.Response;
using WebTest.Transformers.User;

namespace WebTest.Domains.User.Handlers
{
    public class GetList(
        UserRepository userRepository,
        UserTransformer transformer
        ) : IHandler<object, IEnumerable<UserTimeDto>>
    {
        public IEnumerable<UserTimeDto>? Handle(object? dto)
        {
            List<UserTimeDto> response = [];
            var users = userRepository.GetUsers();
            foreach (var user in users)
            {
                response.Add(transformer.Transform(user));
            }

            return response;
        }
    }
}
