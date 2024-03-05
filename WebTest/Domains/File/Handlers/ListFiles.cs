using WebTest.Domains.File.Repositories;
using WebTest.Domains.Interfaces;
using WebTest.Dto.File.Request;
using WebTest.Exeptions.Concrete;
using WebTest.Http.Responses;
using WebTest.Http.Transformers;
using WebTest.Services;
using WebTest.Transformers.File;

namespace WebTest.Domains.File.Handlers
{
    public class ListFiles(
        AuthService authService,
        FileRepository fileRepository,
        FileTransformer transformer)
        : IRequestResponseHandler<ListDto, SuccessDto>
    {
        public SuccessDto Handle(ListDto dto)
        {
            var user = authService.GetCurrentUser() ?? throw new ApiException("User not found", 403);

            var files = fileRepository.GetFiles(user.Id, dto.Page, dto.PerPage);

            return SuccessResponseTransformer.Build(files, transformer);
        }
    }
}
