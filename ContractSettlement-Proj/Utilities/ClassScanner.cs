using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ContractSettlement_Proj.Utilities
{
    public static class ClassScanner
    {
        public static List<string> GetClassNamesFromProject(string projectName, string innerFolder = "")
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var solutionRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));

            var projectPath = Path.Combine(solutionRoot, projectName);

            if (!string.IsNullOrWhiteSpace(innerFolder))
            {
                projectPath = Path.Combine(projectPath, innerFolder);
            }

            if (!Directory.Exists(projectPath))
                throw new DirectoryNotFoundException($"Folder not found: {projectPath}");

            var classNames = new List<string>();
            var csFiles = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories);

            foreach (var file in csFiles)
            {
                var content = File.ReadAllText(file);

                var matches = Regex.Matches(content, @"\b(class|struct|interface)\s+(\w+)", RegexOptions.Multiline);

                foreach (Match match in matches)
                {
                    if (match.Groups.Count > 2)
                    {
                        classNames.Add(match.Groups[2].Value);
                    }
                }
            }

            return classNames;
        }
    }
}