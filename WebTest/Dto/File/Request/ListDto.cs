using Microsoft.AspNetCore.Mvc;

namespace WebTest.Dto.File.Request
{
    public class ListDto
    {
        [FromQuery]
        public int Page { get; set; } = 0;

        [FromQuery]
        public int PerPage { get; set; } = 10;
    }
}
