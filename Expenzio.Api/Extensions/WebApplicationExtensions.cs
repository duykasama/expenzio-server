using Expenzio.Api.Settings;

namespace Expenzio.Api.Extensions;

/// <summary>
/// Contains extension methods for <see cref="WebApplication" />.
/// </summary>
public static class WebApplicationExtensions
{

    /// <summary>
    /// Use Swagger UI with versioning.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    /// <returns>The WebApplication instance.</returns>
    public static WebApplication UseSwaggerUIWithVersioning(this WebApplication app)
    {
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();
            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });
        return app;
    }
    
    /// <summary>
    /// Use request localization.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    /// <returns>The WebApplication instance.</returns>
    public static WebApplication UseCustomRequestLocalization(this WebApplication app)
    {
        var localizationSettings = app.Configuration.GetSection(nameof(LocalizationSettings)).Get<LocalizationSettings>();
        ArgumentNullException.ThrowIfNull(localizationSettings);
        app.UseRequestLocalization(options =>
        {
            options.SetDefaultCulture(localizationSettings.DefaultCulture);
            options.AddSupportedCultures(localizationSettings.SupportedCultures);
            options.AddSupportedUICultures(localizationSettings.SupportedCultures);
        });
        return app;
    }
}
