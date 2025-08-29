using DynamicClassProj.Utilities;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace DynamicClassProj.Models.Test
{
    [SpecialClass]
    public class Employee
    {
        
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal TaxRate { get; set; }
        [Formula("SalaryFormula")]
        public double Salary { get; set; }
        public double TSalary { get; set; }

        // Formula stored as string (could come from DB)
        public string SalaryFormula { get; set; } = "BaseSalary + Bonus - (BaseSalary * TaxRate)";
        public string TSalaryFormula { get; set; } = "Salary*2";

        public decimal CalculateSalary()
        {
            var expr = new Expression(SalaryFormula);

            // Provide values to formula variables
            expr.Parameters["BaseSalary"] = BaseSalary;
            expr.Parameters["Bonus"] = Bonus;
            expr.Parameters["TaxRate"] = TaxRate;

            expr.Parameters["Salary"] = Salary;
            var result = expr.Evaluate();
            return Convert.ToDecimal(result);
        }
    }
}