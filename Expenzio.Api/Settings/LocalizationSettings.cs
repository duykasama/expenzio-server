namespace Expenzio.Api.Settings;

/// <summary>
/// Represents the localization settings.
/// </summary>
/// <remarks>
/// This class is used to store the localization settings.
/// </remarks>
public class LocalizationSettings
{
    public string DefaultCulture { get; set; } = null!;
    public string[] SupportedCultures { get; set; } = null!;
}
