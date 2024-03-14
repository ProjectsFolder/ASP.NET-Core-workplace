using WebTest.Domains.File.Repositories;
using WebTest.Domains.Interfaces;
using WebTest.Dto.File.Command;
using WebTest.Exeptions.Concrete;
using WebTest.Http.Responses;
using WebTest.Http.Transformers;
using WebTest.Models.Files;
using WebTest.Services;

namespace WebTest.Domains.File.Handlers
{
    public class GetFile(
        AuthService authService,
        FileRepository fileRepository)
        : IRequestResponseHandler<GetCommand, UserFile>
    {
        public SuccessDto<UserFile> Handle(GetCommand dto)
        {
            var user = authService.GetCurrentUser() ?? throw new ApiException("User not found", 403);

            var file = fileRepository.GetById(dto.Id) ?? throw new ApiException("File not found", 403);

            if (user.Id != file.UserId)
            {
                throw new ApiException("File not found", 403);
            }

            return SuccessResponseTransformer.Build(file);
        }
    }
}
