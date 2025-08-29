using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

public static class ClassInfoExtractor2
{
    public static Dictionary<string, ClassInfo> GetClassInfo(
        string solutionRelativePath,
        string projectName,
        string targetNamespace)
    {
        // Get solution directory path (4 levels up from bin folder)
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var solutionDir = Path.GetFullPath(Path.Combine(
            baseDirectory,
            "..", "..", "..", "..")); // Adjust based on your project depth

        // Build full assembly path using solution directory
        var projectBinPath = Path.Combine(
            solutionDir,
            solutionRelativePath,
            projectName,
            "bin",
            "Debug"); // Change to Release if needed

        return ScanAssemblies(projectBinPath, targetNamespace);
    }

    private static Dictionary<string, ClassInfo> ScanAssemblies(
        string folderPath,
        string targetNamespace)
    {
        var classInfoDict = new Dictionary<string, ClassInfo>();

        if (!Directory.Exists(folderPath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {folderPath}");
        }

        foreach (var dllPath in Directory.GetFiles(folderPath, "*.dll", SearchOption.AllDirectories))
        {
            try
            {
                var assembly = Assembly.LoadFrom(dllPath);
                var xmlPath = Path.ChangeExtension(dllPath, ".xml");
                var xmlDoc = File.Exists(xmlPath) ? new XmlDocument() : null;
                if (xmlDoc != null) xmlDoc.Load(xmlPath);

                ProcessTypes(assembly, targetNamespace, xmlDoc, classInfoDict);
            }
            catch (BadImageFormatException) { /* Skip native DLLs */ }
            catch (ReflectionTypeLoadException) { /* Skip unloadable types */ }
        }

        return classInfoDict;
    }

    private static void ProcessTypes(
        Assembly assembly,
        string targetNamespace,
        XmlDocument xmlDoc,
        Dictionary<string, ClassInfo> classInfoDict)
    {
        foreach (var type in assembly.GetTypes()
            .Where(t => t.IsClass && t.Namespace == targetNamespace))
        {
            var classInfo = new ClassInfo
            {
                ClassName = type.Name,
                ClassSummary = GetSummaryFromXml(xmlDoc, type),
                Properties = new List<PropertyInfo>()
            };

            ProcessProperties(type, classInfo);

            if (!classInfoDict.ContainsKey(type.FullName))
            {
                classInfoDict.Add(type.FullName, classInfo);
            }
        }
    }
    private static void ProcessProperties(Type type, ClassInfo classInfo)
    {
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
    }

    private static string GetSummaryFromXml(XmlDocument xmlDoc, Type type)
    {
        if (xmlDoc == null) return null;

        string memberName = $"T:{type.FullName}";
        var node = xmlDoc.SelectSingleNode($"//member[@name='{memberName}']/summary");
        return node?.InnerText.Trim();
    }

}

