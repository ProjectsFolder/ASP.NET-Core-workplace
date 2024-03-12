using WebTest.Domains.File.Repositories;
using WebTest.Domains.Interfaces;
using WebTest.Dto.File.Command;
using WebTest.Dto.File.Response;
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
        : IRequestResponseHandler<ListCommand, FileDto>
    {
        public SuccessDto<FileDto> Handle(ListCommand dto)
        {
            var user = authService.GetCurrentUser() ?? throw new ApiException("User not found", 403);

            var files = fileRepository.GetFiles(user.Id, dto.Page, dto.PerPage);

            var meta = new PaginationMeta()
            {
                CurrentPage = files.CurrentPage,
                PerPage = files.PerPage,
                Total = files.Total,
                TotalPages = files.TotalPages,
            };

            return SuccessResponseTransformer.Build(files.Data, transformer, meta);
        }
    }
}
