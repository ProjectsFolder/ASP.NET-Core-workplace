using WebTest.Domains.Time.Interfaces;

namespace WebTest.Domains.Time
{
    public class ShortTimeService : ITimeService
    {
        public string GetTime() => DateTime.Now.ToShortTimeString();
    }
}
