namespace Expenzio.Api.Settings;

/// <summary>
/// Represents the CORS settings.
/// </summary>
/// <remarks>
/// This class is used to store the CORS settings.
/// </remarks>
public class CorsSettings
{
    public CorsPolicy[] Policies { get; init; } = new CorsPolicy[] { };
}

public class CorsPolicy
{
    public string Name { get; init; } = null!;
    public string[] AllowedOrigins { get; init; } = new string[] { };
    public string[] AllowedMethods { get; init; } = new string[] { };
    public string[] AllowedHeaders { get; init; } = new string[] { };
    public bool AllowCredentials { get; init; }
}
