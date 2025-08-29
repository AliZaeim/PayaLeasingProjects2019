using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

public static class DocumentationHelper
{
    private static Dictionary<string, string> _classSummaryCache;

    /// <summary>
    /// Loads the XML documentation and caches it.
    /// </summary>
    private static void LoadDocumentation()
    {
        if (_classSummaryCache != null) return;

        _classSummaryCache = new Dictionary<string, string>();

        // Get the path of the assembly and replace .dll with .xml to get the XML documentation file path
        var assembly = Assembly.GetExecutingAssembly();
        var xmlDocFilePath = Path.ChangeExtension(assembly.Location, ".xml");

        if (!File.Exists(xmlDocFilePath))
            throw new FileNotFoundException($"XML documentation file not found: {xmlDocFilePath}");

        var xml = XDocument.Load(xmlDocFilePath);

        // XML documentation stores members with keys like "T:Namespace.ClassName"
        var members = xml.Descendants("member");

        foreach (var member in members)
        {
            var name = member.Attribute("name")?.Value;
            if (name == null) continue;

            if (name.StartsWith("T:")) // Type (class, interface, struct, enum)
            {
                var className = name.Substring(2); // Remove "T:" prefix
                var summary = member.Element("summary")?.Value.Trim();

                if (!string.IsNullOrWhiteSpace(summary))
                {
                    _classSummaryCache[className] = summary;
                }
            }
        }
    }

    /// <summary>
    /// Gets all class names in the assembly and their summary texts.
    /// </summary>
    /// <returns>Dictionary with class full names as key and summary text as value.</returns>
    public static Dictionary<string, string> GetClassSummaries()
    {
        LoadDocumentation();

        var assembly = Assembly.GetExecutingAssembly();

        var classesInAssembly = assembly.GetTypes()
            .Where(t => t.IsClass)
            .Select(t => t.FullName)
            .Where(fullName => _classSummaryCache.ContainsKey(fullName))
            .ToDictionary(name => name, name => _classSummaryCache[name]);

        return classesInAssembly;
    }
}
