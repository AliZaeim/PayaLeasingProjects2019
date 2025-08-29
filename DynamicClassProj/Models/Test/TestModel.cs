using DynamicClassProj.Utilities;
using NCalc;
using System.Collections.Generic;

namespace DynamicClassProj.Models.Test
{
    [SpecialClass]
    public class TestModel
    {
        public double A { get; set; }
        public double B { get; set; }
        [Formula("C")]
        public double C { get; private set; }
        [Formula("D")]
        public double D { get; private set; }


        public void Calculate(IEnumerable<Formula> formulas)
        {
            var values = new Dictionary<string, object>
            {
                { "A", A },
                { "B", B },
                { "C", C },
                { "D", D }
            };

            foreach (var formula in formulas)
            {
                var expr = new Expression(formula.Expression);


                expr.EvaluateParameter += (name, args) =>
                {
                    if (values.ContainsKey(name))
                        args.Result = values[name];
                };

                var result = expr.Evaluate();

                double resultValue = result != null ? System.Convert.ToDouble(result) : 0;

                values[formula.FieldName] = resultValue;


                switch (formula.FieldName)
                {
                    case "C": C = resultValue; break;
                    case "D": D = resultValue; break;
                }
            }
        }
    }
}