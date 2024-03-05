using WebTest.Domains.File.Repositories;
using WebTest.Domains.Interfaces;
using WebTest.Dto.File.Request;
using WebTest.Exeptions.Concrete;
using WebTest.Models.Files;
using WebTest.Services;
using WebTest.Services.Database;

namespace WebTest.Domains.File.Handlers
{
    public class DeleteFile(
        AuthService authService,
        FileService fileService,
        DatabaseContext databaseContext,
        FileRepository fileRepository)
        : IRequestHandler<DeleteDto>
    {
        public void Handle(DeleteDto dto)
        {
            var user = authService.GetCurrentUser() ?? throw new ApiException("User not found", 403);

            var file = fileRepository.GetById<UserFile>(dto.Id) ?? throw new ApiException("File not found", 403);

            if (user.Id != file.UserId)
            {
                throw new ApiException("File not found", 403);
            }

            databaseContext.Transaction(() =>
            {
                fileRepository.Delete(file);
                if (!fileRepository.HasLinks(file.Path))
                {
                    fileService.Delete(file.Path);
                }
            });
        }
    }
}
