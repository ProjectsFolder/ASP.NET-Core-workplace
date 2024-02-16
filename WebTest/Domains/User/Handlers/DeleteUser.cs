using WebTest.Attributes;
using WebTest.Domains.User.Repositories;
using WebTest.Dto.User.Request;
using WebTest.Exeptions.Concrete;

namespace WebTest.Domains.User.Handlers
{
    [Dependency]
    public class DeleteUser(
        UserRepository userRepository
        ) : IHandler<DeleteDto, object>
    {
        public object? Handle(DeleteDto? dto)
        {
            if (dto == null)
            {
                return null;
            }

            var user = userRepository.GetUser(dto.Id) ?? throw new ApiException("User not found", 404);
            userRepository.Delete(user);

            return null;
        }
    }
}
