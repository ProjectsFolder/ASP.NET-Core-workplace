using WebTest.Domains.Interfaces;
using WebTest.Domains.OrgStructure.Repositories;
using WebTest.Dto.OrgStructure.Command;
using WebTest.Exeptions.Concrete;
using WebTest.Models.OrgStructure;

namespace WebTest.Domains.OrgStructure.Handlers
{
    public class DeleteUser(
        UserRepository userRepository
        ) : IRequestHandler<DeleteCommand>
    {
        public void Handle(DeleteCommand dto)
        {
            var user = userRepository.GetById<User>(dto.Id) ?? throw new ApiException("User not found", 404);
            userRepository.Delete(user);
        }
    }
}
