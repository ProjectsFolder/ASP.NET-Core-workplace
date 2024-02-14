using WebTest.Exeptions.Concrete;
using WebTest.Http.Actions.Time.Request;
using WebTest.Services.Interfaces;

namespace WebTest.Domains.Time
{
    public class GetTimeHandler(ITimeService timeService) : IHandler<TimeRequest, string[]>
    {   
        readonly ITimeService timeService = timeService;

        public string[]? Handle(TimeRequest? dto)
        {
            return [$"Time {dto?.Title} ({dto?.Version}): {timeService.GetTime()}"];
        }
    }
}
