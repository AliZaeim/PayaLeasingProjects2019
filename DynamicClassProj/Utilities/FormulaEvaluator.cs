using DynamicClassProj.Models.Test;
using NCalc;
using System;
using System.Collections.Generic;

namespace DynamicClassProj.Utilities
{
    public class FormulaEvaluator
    {
        private readonly IEnumerable<Formula> _formulas;


        public  FormulaEvaluator(IEnumerable<Formula> formulas)
        {
            // توجه: فرض شده فرمول‌ها به ترتیب محاسبه دلخواه به اینجا داده می‌شوند
            _formulas = formulas;
        }


        
        public  IDictionary<string, double> Evaluate(double a, double b)
        {
            var values = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "A", a },
                { "B", b },
                { "C", 0.0 },
                { "D", 0.0 }
            };


            var results = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);


            foreach (var formula in _formulas)
            {
                var expr = new Expression(formula.Expression);


                expr.EvaluateParameter += (name, args) =>
                {
                    if (values.ContainsKey(name))
                        args.Result = values[name];
                    else
                        args.Result = 0;
                };


                var raw = expr.Evaluate();


                double val = 0;
                try
                {
                    if (raw != null)
                        val = Convert.ToDouble(raw);
                }
                catch
                {
                    val = 0; 
                }


                values[formula.FieldName] = val;
                results[formula.FieldName] = val;
            }


            return results;
        }

    }
}