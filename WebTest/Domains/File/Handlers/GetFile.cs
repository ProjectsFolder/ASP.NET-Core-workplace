using WebTest.Domains.File.Repositories;
using WebTest.Domains.Interfaces;
using WebTest.Dto.File.Request;
using WebTest.Exeptions.Concrete;
using WebTest.Models.Files;
using WebTest.Services;

namespace WebTest.Domains.File.Handlers
{
    public class GetFile(
        AuthService authService,
        FileRepository fileRepository)
        : IRequestResponseHandler<GetDto, UserFile>
    {
        public UserFile Handle(GetDto dto)
        {
            var user = authService.GetCurrentUser() ?? throw new ApiException("User not found", 403);

            var file = fileRepository.GetById<UserFile>(dto.Id) ?? throw new ApiException("File not found", 403);

            if (user.Id != file.UserId)
            {
                throw new ApiException("File not found", 403);
            }

            return file;
        }
    }
}
