using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ContractSettlement_Proj.Utilities
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
                .Where(p => Attribute.IsDefined(p, typeof(HasFormulaAttribute)))
                .ToList();
        }

        public static List<PropInfo> GetFormulaTargetPropertyNames<T>()
        {
            List<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            properties = properties.Where(w => Attribute.IsDefined(w, typeof(HasFormulaAttribute))).ToList();
            //var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
            //string displayName = displayAttr != null ? displayAttr.Name : prop.Name;
            //return GetFormulaTargetProperties<T>().Select(p => p.Name).ToList();
            return properties.Select(p => new PropInfo { PropName = p.Name, PropDisplayName =p.GetCustomAttribute<DisplayAttribute>() != null ? p.GetCustomAttribute<DisplayAttribute>().Name : p.Name }).ToList();
        }
    }

}