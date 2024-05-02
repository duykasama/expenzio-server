using System.Text.Json;
using Microsoft.Extensions.Localization;

namespace Expenzio.Service.Implementation;

public abstract class JsonStringLocalizer : IStringLocalizer
{
    public LocalizedString this[string name]
    {
        get
        {
            var culture = Thread.CurrentThread.CurrentCulture.Name;
            var dict = LoadJsonFile(culture);
            var value = dict.ContainsKey(name) ? dict[name] : name;
            return new LocalizedString(name, value, false);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            return new LocalizedString(name, name, true);
        }
    }

    protected abstract string GetResourceLocation(string culture);

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var culture = Thread.CurrentThread.CurrentCulture.Name;
        var dict = LoadJsonFile(culture);
        var localizedStrings = dict.Select(kvp => new LocalizedString(kvp.Key, kvp.Value, false));
        return localizedStrings;
    }

    private Dictionary<string, string> LoadJsonFile(string culture)
    {
        var jsonFile = File.ReadAllText(GetResourceLocation(culture));
        return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonFile) ?? new Dictionary<string, string>();
    }
}

public class JsonStringLocalizer<T> : JsonStringLocalizer
{
    public JsonStringLocalizer()
    {
    }

    public JsonStringLocalizer(T resource)
    {
    }

    protected override string GetResourceLocation(string culture)
    {
        return $"Resources/{typeof(T).Name}.{culture}.json";
    }
}
