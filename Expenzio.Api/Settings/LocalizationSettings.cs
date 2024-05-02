namespace Expenzio.Api.Settings;

public class LocalizationSettings
{
    public string DefaultCulture { get; set; } = null!;
    public string[] SupportedCultures { get; set; } = null!;
}
