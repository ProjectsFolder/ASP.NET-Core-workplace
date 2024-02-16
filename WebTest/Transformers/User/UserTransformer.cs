using WebTest.Domains.Time.Interfaces;
using WebTest.Dto.User.Response;

namespace WebTest.Transformers.User
{
    public class UserTransformer(ITimeService timeService) : ITransformer<Models.User.User, UserDto>
    {
        public UserDto Transform(Models.User.User from)
        {
            return new UserDto()
            {
                Id = from.Id,
                Login = from.Login,
                Time = timeService.GetTime()
            };
        }
    }
}
