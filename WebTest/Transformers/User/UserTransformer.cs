using WebTest.Domains.Time.Interfaces;
using WebTest.Dto.User.Response;

namespace WebTest.Transformers.User
{
    public class UserTransformer(ITimeService timeService) : ITransformer<Models.User.User, UserTimeDto>
    {
        public UserTimeDto Transform(Models.User.User from)
        {
            return new UserTimeDto()
            {
                UserName = from.Login,
                Time = timeService.GetTime()
            };
        }
    }
}
