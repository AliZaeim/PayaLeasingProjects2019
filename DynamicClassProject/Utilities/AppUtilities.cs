using DynamicClassProject.Attributes;
using DynamicClassProject.Models.Data;
using DynamicClassProject.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace DynamicClassProject.Utilities
{
    public class PropertyData
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public static class AppUtilities
    {

        public class PropertyInfoResult
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public Type PropertyType { get; set; }
            public bool IsNullable { get; set; }
            public string CleanTypeName { get; set; }
            public bool Formula { get; set; }
            public override string ToString()
            {
                return $"{Name} ({DisplayName}) - {CleanTypeName} - Nullable: {IsNullable}";
            }
        }
        public class PropInfo
        {
            public string PropName { get; set; }
            public string PropDisplayName { get; set; }
            public string PropFormula { get; set; }
        }
        public static List<PropertyInfoResult> GetNumericProperties(Type type, bool ExistKey = false)
        {
            var numericTypes = new HashSet<Type>
            {
                typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
                typeof(int), typeof(uint), typeof(long), typeof(ulong),
                typeof(float), typeof(double), typeof(decimal)
            };

            var result = new List<PropertyInfoResult>();

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
        public static List<PropertyInfoResult> GetNumericProperties(string className, bool ExistKey = false)
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
            //properties = properties.Where(w => !w.GetCustomAttributes(typeof(FormulaAttribute), ExistKey).Any()).ToList();
            foreach (var prop in properties)
            {
                Type actualType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                if (numericTypes.Contains(actualType))
                {
                    bool isNullable = Nullable.GetUnderlyingType(prop.PropertyType) != null;

                    var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
                    bool fomu = prop.GetCustomAttribute<FormulaAttribute>() != null;
                    string displayName = displayAttr != null ? displayAttr.Name : prop.Name;

                    result.Add(new PropertyInfoResult
                    {
                        Name = prop.Name,
                        DisplayName = displayName,
                        PropertyType = prop.PropertyType,
                        IsNullable = isNullable,
                        CleanTypeName = actualType.Name,
                        Formula = fomu
                    });
                }
            }

            return result;
        }
        public static bool IsNumericType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type); // unwrap Nullable<T>
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsNumeric(Type fieldType, object value)
        {
            // Handle Nullable<T>
            var underlyingType = Nullable.GetUnderlyingType(fieldType) ?? fieldType;

            if (underlyingType.IsPrimitive && underlyingType != typeof(bool) && underlyingType != typeof(char))
                return true;

            if (underlyingType == typeof(decimal) || underlyingType == typeof(double) || underlyingType == typeof(float))
                return true;

            if (underlyingType == typeof(string))
                return false;

            return false;
        }
        public static List<PropertyInfo> GetFormulaTargetProperties<T>()
        {
            return typeof(T).GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(FormulaAttribute)))
                .ToList();
        }
        public static List<PropInfo> GetFormulaTargetPropertiesWithClassName(string className)
        {
            try
            {
                Type type = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .FirstOrDefault(t => t.Name == className);


                List<PropertyInfo> properties = type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(FormulaAttribute))).ToList();
                return properties.Select(p => new PropInfo { PropName = p.Name, PropDisplayName = p.GetCustomAttribute<DisplayAttribute>() != null ? p.GetCustomAttribute<DisplayAttribute>().Name : p.Name }).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }
        public static List<PropInfo> GetFormulaTargetPropertyNames<T>()
        {
            try
            {
                List<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                properties = properties.Where(w => Attribute.IsDefined(w, typeof(FormulaAttribute))).ToList();
                return properties.Select(p => new PropInfo { PropName = p.Name, PropDisplayName = p.GetCustomAttribute<DisplayAttribute>() != null ? p.GetCustomAttribute<DisplayAttribute>().Name : p.Name }).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }
        public static (bool, string) GetFieldDisplayName(string className, string fieldName)
        {
            Type type = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .FirstOrDefault(t => t.Name == className);
            if (type == null)
            {
                return (false, "Type is null");
            }
            PropertyInfo propertyInfo = type.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
            //DisplayAttribute displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
            string displayText = propertyInfo.GetCustomAttribute<DisplayAttribute>() != null ? propertyInfo.GetCustomAttribute<DisplayAttribute>().Name : propertyInfo.Name;
            return (true, displayText);
        }
        public static (bool, string) GetClassSummary(string className)
        {

            Type type = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(clsassembly => clsassembly.GetTypes())
                        .FirstOrDefault(t => t.Name == className);

            Assembly assembly = type.Assembly;


            // XML doc path (same directory as DLL/EXE)
            string asmPath = assembly.Location; // Full path to DLL/EXE
            string xmlPath = Path.ChangeExtension(asmPath, ".xml");
            if (!File.Exists(xmlPath))
                return (false, null);

            var xml = XDocument.Load(xmlPath);

            // XML member name for class: "T:Namespace.ClassName"
            var fullName = assembly.GetTypes()
                                   .FirstOrDefault(t => t.Name == className)
                                   ?.FullName;
            if (fullName == null)
                return (false, null);

            var memberName = $"T:{fullName}";

            var summaryNode = xml.Descendants("member")
                .FirstOrDefault(m => m.Attribute("name")?.Value == memberName)?
                .Descendants("summary")
                .FirstOrDefault();

            return (true, summaryNode?.Value.Trim());


        }
        public static (bool, string) GetClassDisplayText(string className)
        {
            Type type = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(clsassembly => clsassembly.GetTypes())
                        .FirstOrDefault(t => t.Name == className);
            if (type == null) return (false, null);
            var displayName = type.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                  .Cast<DisplayNameAttribute>()
                                  .FirstOrDefault()?.DisplayName;
            return (true, displayName);
        }
        public static List<PropertyData> MapModelToPropertyData(List<PropertyInputVM> propertyInputVMs)
        {
            List<PropertyData> propertyDatas = new List<PropertyData>();
            foreach (var item in propertyInputVMs)
            {
                propertyDatas.Add(new PropertyData()
                {
                    Name = item.Name,
                    Value = item.Value,
                });
            }

            return propertyDatas;
        }
        public static object MapFromViewModel(Type type, object viewModel)
        {
            // Create instance of the target type
            var instance = Activator.CreateInstance(type);

            var vmProps = viewModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var vmProp in vmProps)
            {
                var targetProp = targetProps.FirstOrDefault(p =>
                    p.Name == vmProp.Name &&
                    p.CanWrite &&
                    p.PropertyType.IsAssignableFrom(vmProp.PropertyType));

                if (targetProp != null)
                {
                    var value = vmProp.GetValue(viewModel);
                    targetProp.SetValue(instance, value);
                }
            }

            return instance;
        }
        public static object MapToInstance(List<PropertyInputVM> Props, Type targetType)
        {
            if (Props == null) throw new ArgumentNullException();

            // Create an instance of the target type
            object instance = Activator.CreateInstance(targetType);

            PropertyInfo[] properties = targetType.GetProperties();
            foreach (var targetProp in properties)
            {

                // Find matching property in ViewModel by name
                //var vmProp = Array.Find(Props.ToArray(), p => p.Name == targetProp.Name);
                var vmProp = Props.FirstOrDefault(f => f.Name == targetProp.Name);
                if (vmProp == null) continue;

                // Get value from ViewModel
                var value = vmProp.Value;

                if (value != null)
                {
                    // Convert value to target property type if needed
                    var convertedValue = ConvertValue(value, targetProp.PropertyType);
                    targetProp.SetValue(instance, convertedValue);
                }
            }

            return instance;
        }
        public static DynamicClassViewModel MapInsatnceToViewModel(object instance, DynamicClassViewModel dynamicClassViewModel)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            Type type = instance.GetType();

            List<PropertyInfo> typePROPS = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            foreach (var prop in typePROPS)
            {
                Type underlyingType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (dynamicClassViewModel.Properties.Any(a => a.Name == prop.Name))
                {
                    PropertyInputVM VMprop = dynamicClassViewModel.Properties.SingleOrDefault(s => s.Name == prop.Name);
                    if (VMprop != null)
                    {

                    }
                }
                dynamicClassViewModel.Properties.Add(new PropertyInputVM()
                {
                    Name = prop.Name,
                    DisplayName = GetFieldDisplayName(type.Name, prop.Name).Item2,
                    IsFormula = prop.IsDefined(typeof(FormulaAttribute)),
                    Order = prop.GetCustomAttribute<DisplayAttribute>()?.GetOrder() ?? prop.MetadataToken,
                    Type = underlyingType.Name,
                    IsNumeric = IsNumericType(prop.PropertyType),
                    Value = prop.GetValue(instance).ToString()
                });
            }
            return dynamicClassViewModel;
        }
        public static object ConvertValue(object value, Type targetType)
        {
            if (value == null) return null;

            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;


            if (underlyingType.IsEnum)
            {
                if (value is string enumStr)
                    return Enum.Parse(underlyingType, enumStr, ignoreCase: true);

                return Enum.ToObject(underlyingType, value);
            }

            if (underlyingType.IsAssignableFrom(value.GetType()))
                return value;

            if (value is string s)
            {
                s = s.Trim().Replace(",", "."); // normalize

                if (underlyingType == typeof(int) && int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var i))
                    return i;

                if (underlyingType == typeof(double) && double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
                    return d;

                if (underlyingType == typeof(decimal) && decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var dec))
                    return dec;

                if (underlyingType == typeof(bool) && bool.TryParse(s, out var b))
                    return b;

                if (underlyingType == typeof(string))
                    return s;
                return null;
            }


            if (underlyingType == typeof(int))
            {
                if (value is double d) return (int)d;
                if (value is decimal dec) return (int)dec;
            }
            if (underlyingType == typeof(double))
            {
                if (value is int i) return (double)i;
                if (value is decimal dec) return (double)dec;
            }
            if (underlyingType == typeof(decimal))
            {
                if (value is int i) return (decimal)i;
                if (value is double d) return (decimal)d;
            }
            if (underlyingType == typeof(bool))
            {
                if (value is int i) return i != 0;
            }
            try
            {
                return Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

    }

}