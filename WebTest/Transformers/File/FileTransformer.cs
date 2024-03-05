using WebTest.Dto.File.Response;
using WebTest.Models.Files;

namespace WebTest.Transformers.File
{
    public class FileTransformer : ITransformer<UserFile, FileDto>
    {
        public FileDto Transform(UserFile from)
        {
            return new FileDto()
            {
                Id = from.Id,
                CreatedAt = from.CreatedAt,
                ContentType = from.ContentType,
                Name = from.Name,
            };
        }
    }
}
