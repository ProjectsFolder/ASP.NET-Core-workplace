using WebTest.Domains.Time.Interfaces;
using WebTest.Dto.Time.Request;
using WebTest.Exeptions.Concrete;

namespace WebTest.Domains.Time.Handlers
{
    public class GetTime(ITimeService timeService) : IHandler<TimeDto, string[]>
    {
        public string[]? Handle(TimeDto? dto)
        {
            return [$"Time {dto?.Title} ({dto?.Version}): {timeService.GetTime()}"];
        }
    }
}
