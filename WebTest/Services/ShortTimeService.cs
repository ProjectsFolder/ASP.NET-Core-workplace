using WebTest.Services.Interfaces;

namespace WebTest.Services
{
    public class ShortTimeService : ITimeService
    {
        public string GetTime() => DateTime.Now.ToShortTimeString();
    }
}
