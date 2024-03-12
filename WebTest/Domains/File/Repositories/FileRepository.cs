using WebTest.Models.Files;
using WebTest.Services.Database;
using WebTest.Services.Database.Dto;
using WebTest.Utils;

namespace WebTest.Domains.File.Repositories
{
    public class FileRepository : RepositoryBase
    {
        public bool HasLinks(string path) => context.Files.Any(f => f.Path == path);

        public Paginator<UserFile> GetFiles(int userId, int page, int perPage)
        {
            return context.Files
                .Where(f => f.UserId == userId)
                .GetPaginator(page, perPage)
            ;
        }
    }
}
