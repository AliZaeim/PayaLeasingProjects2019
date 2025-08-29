namespace ContractSettlement.Models.Test
{
    public class MyClass
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public dynamic Result { get; private set; }

        public void CalculateResult(string expression)
        {
            Result = FormulaEvaluator.EvaluateExpression(expression, A, B,C);
        }
    }
}