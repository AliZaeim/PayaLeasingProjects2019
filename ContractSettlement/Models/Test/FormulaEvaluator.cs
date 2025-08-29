using NCalc;
using System;

namespace ContractSettlement.Models.Test
{
    public static class FormulaEvaluator
    {
        public static dynamic EvaluateExpression(string expression, int a, int b,int c)
        {
            Expression e = new Expression(expression);
            e.Parameters["A"] = a;
            e.Parameters["B"] = b;
            e.Parameters["C"] = c;
            dynamic result = e.Evaluate();
            return result;
        }
    }
}