using System.Security.Cryptography;

namespace WebTest.Utils
{
    public static class StringUtils
    {
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqastuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        }

        public static string CalculateMD5(Stream stream)
        {
            using var md5 = MD5.Create();
            stream.Seek(0, SeekOrigin.Begin);
            var hash = md5.ComputeHash(stream);

            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
