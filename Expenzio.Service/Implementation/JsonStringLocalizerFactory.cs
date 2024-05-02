using Microsoft.Extensions.Localization;

namespace Expenzio.Service.Implementation;

public class JsonStringLocalizerFactory : IStringLocalizerFactory
{
    /// <summary>
    /// Creates a new <see cref="IStringLocalizer"/> for a given <paramref name="resourceSource"/>.
    /// </summary>
    /// <param name="resourceSource">The <see cref="Type"/> to create an <see cref="IStringLocalizer"/> for.</param>
    /// <returns>A new <see cref="IStringLocalizer"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="resourceSource"/> is null.</exception>
    public IStringLocalizer Create(Type resourceSource)
    {
        Type genericType = typeof(JsonStringLocalizer<>).MakeGenericType(resourceSource);
        var instance = Activator.CreateInstance(genericType) as IStringLocalizer;
        ArgumentNullException.ThrowIfNull(instance, nameof(instance));
        return instance;
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        // TODO: Implement this method
        return null!;
    }
}
