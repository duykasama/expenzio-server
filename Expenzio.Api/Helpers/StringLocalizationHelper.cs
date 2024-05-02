using System.Resources.NetStandard;
using System.Text;
using System.Text.Json;
using Path = System.IO.Path;

namespace Expenzio.Api.Helpers;

public static class StringLocalizationHelper
{
    private const string XmlHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

    /// <summary>
    /// Generate .resx files from .json files.
    /// </summary>
    /// <param name="sourceDir">The source directory containing the .json files.</param>
    /// <param name="destinationDir">The destination directory to save the .resx files.</param>
    public static void GenerateResxFilesFromJson(string sourceDir, string destinationDir)
    {
        var jsonFiles = Directory.GetFiles(sourceDir, "*.json");
        foreach (var jsonFile in jsonFiles)
        {
            var jsonFileContent = File.ReadAllText(jsonFile);
            var jsonDict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonFileContent);
            if (jsonDict == null) continue;
            var fileName = Path.GetFileName(jsonFile);
            var resxFileName = Path.Combine(destinationDir, fileName.Replace(".json", ".resx"));
           using var resxFile = new ResXResourceWriter(resxFileName);
            foreach (var (key, value) in jsonDict)
            {
                resxFile.AddResource(key, value);

            }
        }
    }

    /// <summary>
    /// Append a string to the StringBuilder and then append a new line.
    /// </summary>
    /// <param name="stringBuilder">The StringBuilder instance.</param>
    /// <param name="s">The string to append.</param>
    private static StringBuilder AppendThenNewLine(this StringBuilder stringBuilder, string s) => stringBuilder.Append(s).AppendLine();

    /// <summary>
    /// Append a tab to the StringBuilder.
    /// </summary>
    /// <param name="stringBuilder">The StringBuilder instance.</param>
    private static StringBuilder AppendTab(this StringBuilder stringBuilder) => stringBuilder.Append("\t");
    
    /// <summary>
    /// Append a number of tabs to the StringBuilder.
    /// </summary>
    /// <param name="stringBuilder">The StringBuilder instance.</param>
    private static StringBuilder AppendTab(this StringBuilder stringBuilder, int tabCount)
    {
        for (var i = 0; i < tabCount; i++)
        {
            stringBuilder.Append("\t");
        }
        return stringBuilder;
    }
}
