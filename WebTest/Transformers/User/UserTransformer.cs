using WebTest.Domains.Time.Interfaces;
using WebTest.Dto.User.Response;

namespace WebTest.Transformers.User
{
    public class UserTransformer(ITimeService timeService) : ITransformer<Models.OrgStructure.User, UserDto>
    {
        public UserDto Transform(Models.OrgStructure.User from)
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
