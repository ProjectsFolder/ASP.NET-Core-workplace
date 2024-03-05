using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTest.Domains.File.Handlers;
using WebTest.Dto.File.Request;
using WebTest.Security.Authentication.UserToken;

namespace WebTest.Http.Controllers.File
{
    [Authorize(AuthenticationSchemes = UserTokenDefaults.SchemaName)]
    public class FilesController : AppController
    {
        [HttpGet]
        public IActionResult List(ListDto request, ListFiles handler)
        {
            return Success(handler, request);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Download(int id, GetFile handler)
        {
            var request = new GetDto { Id = id };
            var file = handler.Handle(request);

            return PhysicalFile(file.Path, file.ContentType, file.Name);
        }

        [HttpPost]
        public IActionResult Create(CreateDto request, CreateFile handler)
        {
            return Success(handler, request);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id, DeleteFile handler)
        {
            var request = new DeleteDto { Id = id };

            return Success(handler, request);
        }
    }
}
