namespace Application.Utils;

public static class Retry
{
    public static T Execute<T>(Func<T> func, int retries, int millisecondsTimeout = 1000)
    {
        while (true)
        {
            try
            {
                return func();
            }
            catch
            {
                if (--retries == 0)
                {
                    throw;
                }
                Thread.Sleep(millisecondsTimeout);
            }
        }
    }
}
