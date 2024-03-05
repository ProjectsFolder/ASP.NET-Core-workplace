using WebTest.Attributes;
using WebTest.Utils;

namespace WebTest.Services
{
    [Service(type: ServiceType.Singleton)]
    public sealed class FileService(IWebHostEnvironment environment)
    {
        private readonly IWebHostEnvironment _environment = environment;

        public string Save(Stream stream, string store = "default")
        {
            var uniqueFileName = StringUtils.CalculateMD5(stream);
            var storagePath = Path.Combine(_environment.WebRootPath, store, uniqueFileName[0..2], uniqueFileName[2..4]);
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }
            var filePath = Path.Combine(storagePath, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);

            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);

            return filePath;
        }

        public bool Delete(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
