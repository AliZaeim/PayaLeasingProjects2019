using DynamicClassProject.Attributes;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DynamicClassProject.Utilities
{
    public static class GenericFormulaEvaluator
    {
        /// <summary>
        /// Evaluate all properties marked with [Formula] for a given object.
        /// </summary>
        public static void EvaluateFormulas<T>(T obj, Dictionary<string, string> formulas)
        {
            Type type = typeof(T);
            IEnumerable<PropertyInfo> formulaProps = type.GetProperties()
                                   .Where(p => p.GetCustomAttribute<FormulaAttribute>() != null);

            foreach (var prop in formulaProps)
            {
                if (formulas.TryGetValue(prop.Name, out string formula))
                {
                    var expression = new Expression(formula);
                    // Assign all other property values as variables
                    foreach (var variableProp in type.GetProperties())
                    {
                        var value = variableProp.GetValue(obj);
                        expression.Parameters[variableProp.Name] = value ?? 0;
                    }
                    var result = expression.Evaluate();
                    prop.SetValue(obj, Convert.ToDouble(result));
                }
            }
        }
        public static object Evaluate(string className, object MyInstance, Dictionary<string, string> formulas)
        {
            // Find the type dynamically by class name
            //var type = Type.GetType(className, throwOnError: true);
            Type type = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .FirstOrDefault(t => t.Name == className);
            IEnumerable<PropertyInfo> formulaProps = type.GetProperties();
                                   
            foreach (var prop in formulaProps)
            {
                if (formulas.TryGetValue(prop.Name, out string zformula))
                {
                    var expression = new Expression(zformula);
                    // Assign all other property values as variables
                    foreach (var variableProp in type.GetProperties())
                    {
                        var value = variableProp.GetValue(MyInstance);
                        expression.Parameters[variableProp.Name] = value ?? 0;
                    }
                    var result = expression.Evaluate();
                    Type targetType = Nullable.GetUnderlyingType(prop.PropertyType)
                                          ?? prop.PropertyType;
                    if (targetType != null)
                    {
                        if (targetType.IsEnum)
                        {
                            prop.SetValue(MyInstance, Enum.ToObject(targetType, result));
                        }
                        else
                        {
                            object converted = Convert.ChangeType(result, targetType);
                            prop.SetValue(MyInstance, converted);
                        }
                    }
                    else
                    {
                        prop.SetValue(MyInstance, null);
                    }

                }
            }
            return MyInstance;

        }

        public static T BuildFromDictionary<T>(string className, object MyInstance, Dictionary<string, string> values)
        where T : new()
        {
            var instance = new T();
            var type = typeof(T);

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanWrite)
                    continue;

                // get input value if exists
                string input;
                values.TryGetValue(prop.Name, out input);

                // check if property has [Formula]
                bool isFormula = prop.GetCustomAttribute<FormulaAttribute>() != null;

                if (isFormula && !string.IsNullOrEmpty(input))
                {
                    // Build NCalc expression
                    var expr = new Expression(input);

                    // supply variables from already-set properties
                    foreach (var otherProp in type.GetProperties())
                    {
                        if (otherProp.CanRead)
                        {
                            var val = otherProp.GetValue(instance);
                            expr.Parameters[otherProp.Name] = val;
                        }
                    }

                    var result = expr.Evaluate();

                    // convert result to property type
                    object converted = Convert.ChangeType(result, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    prop.SetValue(instance, converted);
                }
                else if (!string.IsNullOrEmpty(input))
                {
                    // Normal assignment (string/number)
                    object converted = Convert.ChangeType(input, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    prop.SetValue(instance, converted);
                }
            }

            return instance;
        }
        public static Dictionary<string, object> FinalEvaluate(string className, Dictionary<string, string> formulas)
        {

            var results = new Dictionary<string, object>();

            if (string.IsNullOrWhiteSpace(className))
                throw new ArgumentNullException(nameof(className));

            // پیدا کردن نوع کلاس
            var type = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes())
                        .FirstOrDefault(t => t.Name == className);

            if (type == null)
                throw new ArgumentException($"Class {className} not found in loaded assemblies");

            // ساخت نمونه خالی از کلاس
            var model = Activator.CreateInstance(type);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                // فقط پراپرتی‌هایی که [Formula] دارن
                var hasFormula = prop.GetCustomAttributes(typeof(FormulaAttribute), false).Any();
                if (!hasFormula) continue;

                // دنبال فرمول بگردیم
                if (!formulas.TryGetValue(prop.Name, out string formula)) continue;

                var expr = new Expression(formula);

                // همه پراپرتی‌ها رو به عنوان پارامتر به فرمول بدیم
                foreach (var p in props)
                {
                    var val = p.GetValue(model);
                    expr.Parameters[p.Name] = val ?? 0;
                }

                var value = expr.Evaluate();

                // مقدار محاسبه شده رو داخل شیء اصلی هم ست کنیم
                if (value != null && prop.CanWrite)
                {
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    var convertedValue = Convert.ChangeType(value, targetType);
                    prop.SetValue(model, convertedValue);
                }

                results[prop.Name] = value;
            }

            return results;
        }

    }
}