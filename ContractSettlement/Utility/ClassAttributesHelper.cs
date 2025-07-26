using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace ContractSettlement.Utility
{
    public static class ClassAttributesHelper
    {
        public static string GetDisplayName(object obj, string propertyName)
        {
            return obj == null ? null : GetDisplayName(obj.GetType(), propertyName);
        }

        public static string GetDisplayName(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName);

            return property == null ? null : GetDisplayName(property);
        }

        public static string GetDisplayName(PropertyInfo property)
        {
            var attrName = GetAttributeDisplayName(property);

            if (!string.IsNullOrEmpty(attrName))
            {
                return attrName;
            }

            var metaName = GetMetaDisplayName(property);

            return !string.IsNullOrEmpty(metaName) ? metaName : property.Name;
        }

        private static string GetAttributeDisplayName(PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);

            return attributes.Length == 0
                ? null
                : (attributes[0] as DisplayNameAttribute)?.DisplayName;
        }

        private static string GetMetaDisplayName(PropertyInfo property)
        {
            if (property.DeclaringType != null)
            {
                var attributes = property.DeclaringType.GetCustomAttributes(typeof(MetadataTypeAttribute), true);
                if (attributes.Length == 0)
                {
                    return null;
                }

                if (attributes[0] is MetadataTypeAttribute metaAttr)
                {
                    var metaProperty = metaAttr.MetadataClassType.GetProperty(property.Name);
                    return metaProperty == null
                        ? null
                        : GetAttributeDisplayName(metaProperty);
                }
            }

            return null;
        }
        public static class GetEntityDisplayName<TModel>
        {
            /// <summary>
            /// get DisplayAttribute Name of a property of Type
            /// </summary>
            public static string Display<TProperty>(Expression<Func<TModel, TProperty>> f)
            {
                MemberExpression exp = f.Body as MemberExpression;
                var property = exp?.Expression?.Type.GetProperty(exp.Member.Name);
                var attr = property?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attr?.GetName() ?? exp?.Member.Name;
            }
        }


        public static Dictionary<string, string> GetPropertyNamesAndDisplayNames<T>() where T : class
        {

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (var property in properties)
            {

                DisplayAttribute displayAttribute = property.GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .FirstOrDefault() as DisplayAttribute;
                if (displayAttribute != null)
                {
                    result.Add(property.Name, displayAttribute.Name);
                }
                else
                {
                    result.Add(property.Name, property.Name);
                }

            }

            return result;
        }
        /// <summary>
        /// Get an Enity 
        /// <para>Property Name</para>
        /// <para>Display Title</para>
        /// <para>Attribute Value</para>
        /// <para>Order in Entity</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <param name="ShowKeyItem"></param>
        /// <returns></returns>
        public static List<(string propertyName, string DisplayTitle, dynamic AttributeValue, int Order)> GetObjectPropertyNameAndDisplayNameAndValue<T>(this T Obj, bool ShowKeyItem = false) where T : class, new()
        {
            List<(string propertyName, string AttributeName, dynamic AttributeValue, int Ord)> ProNamesValues = new List<(string propertyName, string AttributeName, dynamic AttributeValue, int Ord)>();

            List<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            foreach (PropertyInfo property in properties.OrderBy(x => x.GetCustomAttribute<DisplayAttribute>()?.GetOrder() ?? int.MaxValue))
            {
                int Porder = property.GetCustomAttribute<DisplayAttribute>()?.GetOrder() ?? int.MaxValue;
                dynamic Value = property.GetValue(Obj, null);

                if (property.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() is DisplayAttribute displayAttribute)
                {

                    if (property.IsKey())
                    {
                        if (ShowKeyItem)
                        {
                            ProNamesValues.Add((property.Name, displayAttribute.Name, Value, Porder));
                        }
                    }
                    else
                    {
                        ProNamesValues.Add((property.Name, displayAttribute.Name, Value, Porder));
                    }

                }
                //else
                //{
                //    ProNamesValues.Add((property.Name, property.Name, Value!));
                //}

            }
            return ProNamesValues;

        }

        /// <summary>
        /// دریافت نام فیلد، عنوان و مقدار نمایشی فیلد
        /// /// <para>یک کلاس</para>
        /// <para>به صورت صف</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public static Queue<(string propertyName, string DisplayName, dynamic AttributeValue)> GetObjectPropertyNameAndDisplayNameAndValueAsQueue<T>(T Obj) where T : class, new()
        {

            Queue<(string propertyName, string AttributeName, dynamic AttibuteValu)> QPropNamesValues = new Queue<(string propertyName, string AttributeName, dynamic AttibuteValu)>();
            var properties = typeof(T)
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {

                DisplayAttribute displayAttribute = property
                                                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                                                     .FirstOrDefault() as DisplayAttribute;
                dynamic Value = property.GetValue(Obj, null);

                if (displayAttribute != null)
                {
                    QPropNamesValues.Enqueue((property.Name, displayAttribute.Name, Value));
                }
                else
                {
                    QPropNamesValues.Enqueue((property.Name, property.Name, Value));
                }

            }
            return QPropNamesValues;

        }
        public static bool IsKey(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false).Any();
        }
        public static bool IsKey(this PropertyDescriptor propertyDescriptor)
        {
            return propertyDescriptor.Attributes[typeof(KeyAttribute)] != null;
        }
        public static List<string> GetPropertiesName<T>(bool GetKeyprop = false) where T : class, new()
        {
            List<string> names = new List<string>();
            List<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            foreach (PropertyInfo property in properties.OrderBy(x => x.GetCustomAttribute<DisplayAttribute>()?.GetOrder() ?? int.MaxValue))
            {
                if (property.GetCustomAttributes(typeof(KeyAttribute), false).Any())
                {
                    if (GetKeyprop)
                    {
                        names.Add(property.Name);
                    }

                }
                else
                {
                    names.Add(property.Name);
                }

            }
            return names;
        }
    }
}