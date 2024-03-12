using WebTest.Domains.File.Repositories;
using WebTest.Domains.Interfaces;
using WebTest.Dto.File.Command;
using WebTest.Dto.File.Response;
using WebTest.Exeptions.Concrete;
using WebTest.Http.Responses;
using WebTest.Http.Transformers;
using WebTest.Models.Files;
using WebTest.Services;
using WebTest.Transformers.File;

namespace WebTest.Domains.File.Handlers
{
    public class CreateFile(
        AuthService authService,
        FileService fileService,
        FileRepository fileRepository,
        FileTransformer transformer)
        : IRequestResponseHandler<CreateCommand, FileDto>
    {
        public SuccessDto<FileDto> Handle(CreateCommand dto)
        {
            var user = authService.GetCurrentUser() ?? throw new ApiException("User not found", 403);

            using var stream = dto.File.OpenReadStream();
            var path = fileService.Save(stream);

            var file = new UserFile()
            {
                CreatedAt = DateTime.UtcNow,
                Name = Path.GetFileName(dto.File.FileName),
                ContentType = dto.File.ContentType,
                UserId = user.Id,
                Path = path,
            };

            fileRepository.Save(file);

            return SuccessResponseTransformer.Build(file, transformer);
        }
    }
}
