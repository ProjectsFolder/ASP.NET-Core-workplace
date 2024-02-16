namespace WebTest.Utils
{
    public static class StringUtils
    {
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
