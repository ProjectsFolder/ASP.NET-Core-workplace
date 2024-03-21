using System.Security.Cryptography;

namespace Application.Utils;

public static class StringUtils
{
    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqastuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
    }
}
