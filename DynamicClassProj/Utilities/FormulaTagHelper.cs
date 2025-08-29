using DynamicClassProj.Utilities.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DynamicClassProj.Utilities
{
    public static class FormulaTargetHelper
    {
        public class PropInfo
        {
            public string PropName { get; set; }
            public string PropDisplayName { get; set; }
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
    }
}