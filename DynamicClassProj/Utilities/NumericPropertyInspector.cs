using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DynamicClassProj.Utilities
{
    public static class NumericPropertyInspector
    {
        public class PropertyInfoResult
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public Type PropertyType { get; set; }
            public bool IsNullable { get; set; }
            public string CleanTypeName { get; set; }

            public override string ToString()
            {
                return $"{Name} ({DisplayName}) - {CleanTypeName} - Nullable: {IsNullable}";
            }
        }

        public static List<PropertyInfoResult> GetNumericProperties(Type type, bool ExistKey= false)
        {
            var numericTypes = new HashSet<Type>
            {
                typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
                typeof(int), typeof(uint), typeof(long), typeof(ulong),
                typeof(float), typeof(double), typeof(decimal)
            };

            var result = new List<PropertyInfoResult>();

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList() ;
            properties = properties.Where(w => !w.GetCustomAttributes(typeof(KeyAttribute), ExistKey).Any()).ToList();
            properties = properties.Where(w => !w.GetCustomAttributes(typeof(FormulaAttribute), ExistKey).Any()).ToList();
            foreach (var prop in properties)
            {
                Type actualType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                if (numericTypes.Contains(actualType))
                {
                    bool isNullable = Nullable.GetUnderlyingType(prop.PropertyType) != null;

                    var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
                    string displayName = displayAttr != null ? displayAttr.Name : prop.Name;

                    result.Add(new PropertyInfoResult
                    {
                        Name = prop.Name,
                        DisplayName = displayName,
                        PropertyType = prop.PropertyType,
                        IsNullable = isNullable,
                        CleanTypeName = actualType.Name
                    });
                }
            }

            return result;
        }
        public static List<PropertyInfoResult> GetNumericProperties(string className, bool ExistKey =  false)
        {
            var numericTypes = new HashSet<Type>
            {
                typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
                typeof(int), typeof(uint), typeof(long), typeof(ulong),
                typeof(float), typeof(double), typeof(decimal)
            };

            var result = new List<PropertyInfoResult>();
            Type type = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .FirstOrDefault(t => t.Name == className);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            properties = properties.Where(w => !w.GetCustomAttributes(typeof(KeyAttribute), ExistKey).Any()).ToList();
            properties = properties.Where(w => !w.GetCustomAttributes(typeof(FormulaAttribute), ExistKey).Any()).ToList();
            foreach (var prop in properties)
            {
                Type actualType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                if (numericTypes.Contains(actualType))
                {
                    bool isNullable = Nullable.GetUnderlyingType(prop.PropertyType) != null;

                    var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
                    string displayName = displayAttr != null ? displayAttr.Name : prop.Name;

                    result.Add(new PropertyInfoResult
                    {
                        Name = prop.Name,
                        DisplayName = displayName,
                        PropertyType = prop.PropertyType,
                        IsNullable = isNullable,
                        CleanTypeName = actualType.Name
                    });
                }
            }

            return result;
        }
    }
}