using DynamicClassProj.Models.Data;
using NCalc;
using System;
using System.Linq;

namespace DynamicClassProj.Utilities
{
    public static class FormulaCalculator
    {
        public static void ComputeFormulas<T>(T data, AppDbContext db) where T : class 
        {
            //var formulas = db.FormulaDefinitions.ToList();

            //foreach (var formulaDef in formulas)
            //{
            //    var expr = new Expression(formulaDef.FormulaText);

            //    // مقداردهی پارامترها
            //    var props = typeof(T).GetProperties();
            //    foreach (var p in props)
            //    {
            //        expr.Parameters[p.Name] = p.GetValue(data) ?? 0;
            //    }

            //    var result = expr.Evaluate();

            //    var propToSet = typeof(T).GetProperty(formulaDef.FieldName);
            //    if (propToSet != null && propToSet.CanWrite)
            //    {
            //        propToSet.SetValue(data, Convert.ChangeType(result, propToSet.PropertyType));
            //    }
            //}
        }
    }
}
