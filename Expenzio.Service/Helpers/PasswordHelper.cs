using BCryptHasher = BCrypt.Net.BCrypt;

namespace Expenzio.Service.Helpers;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
        return BCryptHasher.HashPassword(password);
    }

    public static bool VerifyPassword(string rawPassword, string hash)
    {
        try
        {
            return BCryptHasher.Verify(rawPassword, hash);
        }
        catch
        {
            return false;
        }
    }
}
