using WebTest.Domains.Time.Interfaces;
using WebTest.Dto.User.Response;

namespace WebTest.Transformers.User
{
    public class UserTransformer(ITimeService timeService) : ITransformer<Models.User, UserTimeDto>
    {
        public UserTimeDto Transform(Models.User from)
        {
            return new UserTimeDto()
            {
                UserName = from.Name,
                Time = timeService.GetTime()
            };
        }
    }
}
