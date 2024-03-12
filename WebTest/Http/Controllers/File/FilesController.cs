using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.File.Handlers;
using WebTest.Dto.File.Command;
using WebTest.Dto.File.Request;
using WebTest.Dto.File.Response;
using WebTest.Exeptions.Concrete;
using WebTest.Http.Responses;
using WebTest.Security.Authentication.UserToken;

namespace WebTest.Http.Controllers.File
{
    [Authorize(AuthenticationSchemes = UserTokenDefaults.SchemaName)]
    public class FilesController : AppController
    {
        [HttpGet]
        [ProducesResponseType<SuccessItemsWithMeta<FileDto, ListMetaDto>>(200)]
        public IActionResult List(ListDto request, ListFiles handler)
        {
            var command = new ListCommand
            {
                Page = request.Page,
                PerPage = request.PerPage,
            };

            return Success(handler, command);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Download(int id, GetFile handler)
        {
            var command = new GetCommand { Id = id };
            var file = handler.Handle(command).Item ?? throw new ApiException("File not found", 404);
            var stream = System.IO.File.OpenRead(file.Path);

            return File(stream, file.ContentType, file.Name);
        }

        [HttpPost]
        [ProducesResponseType<SuccessItem<FileDto>>(200)]
        public IActionResult Create(CreateDto request, CreateFile handler)
        {
            var command = new CreateCommand { File = request.File };

            return Success(handler, command);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, DeleteFile handler)
        {
            var command = new DeleteCommand { Id = id };

            return Success(handler, command);
        }
    }
}
