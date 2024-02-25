using WebTest.Domains.Interfaces;
using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.OrgStructure.Request;
using WebTest.Exeptions.Concrete;

namespace WebTest.Domains.OrgStructure.Handlers
{
    public class DeleteUser(
        UserRepository userRepository
        ) : IRequestHandler<DeleteDto>
    {
        public void Handle(DeleteDto dto)
        {
            var user = userRepository.GetUser(dto.Id) ?? throw new ApiException("User not found", 404);
            userRepository.Delete(user);
        }
    }
}
