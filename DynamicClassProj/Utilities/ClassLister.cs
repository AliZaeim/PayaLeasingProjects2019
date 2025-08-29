﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DynamicClassProj.Utilities
{
    public static class ClassLister
    {
        // Get class names from a specific namespace (folder) in the current assembly
        public static List<string> GetClassNamesInNamespace(string targetNamespace, bool includeSubNamespaces = true)
        {
            Assembly assembly = Assembly.GetExecutingAssembly(); // Use current project's assembly
            return GetClassNamesInNamespace(assembly, targetNamespace, includeSubNamespaces);
        }

        // Get class names from a specific namespace (folder) in a given assembly
        public static List<string> GetClassNamesInNamespace(Assembly assembly, string targetNamespace, bool includeSubNamespaces = true)
        {
            try
            {
                IEnumerable<Type> types = assembly.GetTypes()
                    .Where(t => t.IsClass); // Only include classes (not structs, interfaces, etc.)

                // Filter by namespace
                if (includeSubNamespaces)
                    types = types.Where(t =>
                        t.Namespace != null &&
                        (t.Namespace.Equals(targetNamespace) ||
                         t.Namespace.StartsWith(targetNamespace + ".")));
                else
                    types = types.Where(t =>
                        t.Namespace != null &&
                        t.Namespace.Equals(targetNamespace));

                return types.Select(t => t.Name) // Use t.FullName for full namespace
                           .ToList();
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Handle partially loaded assemblies
                return ex.Types.Where(t => t != null)
                               .Select(t => t.Name)
                               .ToList();
            }
        }
    }
}