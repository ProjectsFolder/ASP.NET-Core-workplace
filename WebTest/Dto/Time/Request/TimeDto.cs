using System.ComponentModel.DataAnnotations;

namespace WebTest.Dto.Time.Request
{
    public class TimeDto(string? title, string? version)
    {
        [Required]
        public string? Title { get; set; } = title;

        public string? Version { get; set; } = version;
    }
}
