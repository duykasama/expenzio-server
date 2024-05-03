namespace Expenzio.Service.Settings;

/// <summary>
/// Represents the JWT settings.
/// </summary>
/// <remarks>
/// This class is used to store the JWT settings.
/// </remarks>
public class JwtSettings
{
    public string SigningKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int AccessTokenLifetimeInMinutes { get; set; }
    public int RefreshTokenLifetimeInMinutes { get; set; }
}
