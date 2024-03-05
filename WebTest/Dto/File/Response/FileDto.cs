namespace WebTest.Dto.File.Response
{
    public class FileDto
    {
        public required int Id { get; set; }

        public required DateTime CreatedAt { get; set; }

        public required string Name { get; set; }

        public required string ContentType { get; set; }
    }
}
