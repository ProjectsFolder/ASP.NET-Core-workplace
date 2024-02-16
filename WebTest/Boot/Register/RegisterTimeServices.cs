using WebTest.Domains.Time;
using WebTest.Domains.Time.Interfaces;

namespace WebTest.Boot.Register
{
    public static class RegisterTimeServices
    {
        public static void AddTimeServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ITimeService, ShortTimeService>();
        }
    }
}
