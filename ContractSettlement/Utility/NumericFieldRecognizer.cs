using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ContractSettlement.Utility
{
    public static class NumericFieldScanner
    {
        // Core numeric types (non-nullable)
        private static readonly HashSet<Type> CoreNumericTypes = new HashSet<Type>
    {
        typeof(sbyte), typeof(byte),
        typeof(short), typeof(ushort),
        typeof(int), typeof(uint),
        typeof(long), typeof(ulong),
        typeof(float), typeof(double), typeof(decimal)
    };

        // Static method to scan numeric fields with nullability state
        public static IEnumerable<(string FieldName, Type FieldType, bool IsNullable)> ScanNumericFields(Type targetType)
        {
            if (targetType == null)
                throw new ArgumentNullException(nameof(targetType));

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                 BindingFlags.Instance | BindingFlags.Static;

            foreach (var field in targetType.GetFields(flags))
            {
                var typeInfo = GetNumericTypeInfo(field.FieldType);
                if (typeInfo.IsNumeric)
                {
                    
                    int l = "k__BackingField".Length;
                    string FieldName = field.Name;
                    if (field.Name.Contains("k__BackingField"))
                    {
                        FieldName = field.Name.Remove(field.Name.Length-l-1, l-1);
                        FieldName = FieldName.Remove(0,1);
                    }
                    yield return (FieldName, field.FieldType, typeInfo.IsNullable);
                }
            }
        }

        // Get detailed numeric type information
        private static (bool IsNumeric, bool IsNullable) GetNumericTypeInfo(Type type)
        {
            // Check for standard numeric types
            if (CoreNumericTypes.Contains(type))
                return (IsNumeric: true, IsNullable: false);

            // Handle nullable numeric types
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type underlyingType = Nullable.GetUnderlyingType(type);
                if (CoreNumericTypes.Contains(underlyingType))
                    return (IsNumeric: true, IsNullable:true);
            }

            return (false, false);
        }
    }
}