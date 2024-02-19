using WebTest.Domains.Time.Interfaces;

namespace WebTest.Domains.Time
{
    public class TimeService : ITimeService
    {
        public string GetTime() => DateTime.Now.ToLongTimeString();
    }
}
