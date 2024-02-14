using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebTest.Http.Actions.Time.Request
{
    public class TimeRequest(string? title, string? version)
    {
        [Required]
        public string? Title { get; set; } = title;

        public string? Version { get; set; } = version;
    }
}
