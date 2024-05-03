using BCryptHasher = BCrypt.Net.BCrypt;

namespace Expenzio.Service.Helpers;

/// <summary>
/// Represents the password helper.
/// </summary>
/// <remarks>
/// This class is used to hash and verify passwords.
/// </remarks>
public static class PasswordHelper
{
    /// <summary>
    /// Hashes the given password.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The hashed password.</returns>
    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
        return BCryptHasher.HashPassword(password);
    }

    /// <summary>
    /// Verifies the given password with the hash.
    /// </summary>
    /// <param name="rawPassword">The raw password to verify.</param>
    /// <param name="hash">The hash to verify against.</param>
    /// <returns>True if the password is verified, otherwise false.</returns>
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
