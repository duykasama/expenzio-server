using System.Security.Cryptography;
using System.Text;

namespace Expenzio.Service.Helpers;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        var data = Encoding.UTF8.GetBytes(password);
        var hashData = new HMACSHA3_256().ComputeHash(data);
        return Encoding.UTF8.GetString(hashData);
    }

    public static bool VerifyPassword(string rawPassword, string hash)
    {
        return HashPassword(rawPassword) == hash;
    }
}
