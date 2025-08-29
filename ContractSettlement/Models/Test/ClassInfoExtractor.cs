using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

public static class ClassInfoExtractor
{
    public static Dictionary<string, ClassInfo> GetClassInfo(string folderPath, string targetNamespace)
    {
        var classInfoDict = new Dictionary<string, ClassInfo>();

        // Get all DLLs in the folder
        foreach (var dllPath in Directory.GetFiles(folderPath, "*.dll", SearchOption.AllDirectories))
        {
            try
            {
                var assembly = Assembly.LoadFrom(dllPath);
                var xmlPath = Path.ChangeExtension(dllPath, ".xml");
                var xmlDoc = File.Exists(xmlPath) ? new XmlDocument() : null;
                if (xmlDoc != null) xmlDoc.Load(xmlPath);

                // Scan classes in the target namespace
                foreach (var type in assembly.GetTypes().Where(t =>
                    t.IsClass &&
                    t.Namespace == targetNamespace))
                {
                    var classInfo = new ClassInfo
                    {
                        ClassName = type.Name,
                        ClassSummary = GetSummaryFromXml(xmlDoc, type),
                        Properties = new List<PropertyInfo>()
                    };

                    // Get properties with [Display] attributes
                    foreach (var prop in type.GetProperties())
                    {
                        var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
                        if (displayAttr != null)
                        {
                            classInfo.Properties.Add(new PropertyInfo
                            {
                                PropertyName = prop.Name,
                                DisplayName = displayAttr.Name
                            });
                        }
                    }

                    classInfoDict[type.FullName] = classInfo;
                }
            }
            catch { /* Skip unloadable assemblies */ }
        }

        return classInfoDict;
    }

    private static string GetSummaryFromXml(XmlDocument xmlDoc, Type type)
    {
        if (xmlDoc == null) return null;

        string memberName = $"T:{type.FullName}";
        var node = xmlDoc.SelectSingleNode($"//member[@name='{memberName}']/summary");
        return node?.InnerText.Trim();
    }
}

public class ClassInfo
{
    public string ClassName { get; set; }
    public string ClassSummary { get; set; }
    public List<PropertyInfo> Properties { get; set; }
}

public class PropertyInfo
{
    public string PropertyName { get; set; }
    public string DisplayName { get; set; }
}