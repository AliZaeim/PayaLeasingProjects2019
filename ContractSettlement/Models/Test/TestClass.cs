using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContractSettlement.Models.Test
{
    public class TestClass
    {
        [Display(Name = "حقوق روزانه")]
        public int Price { get; set; }
        [Display(Name = "تعداد روزهای کارکرد")]
        public int Days { get; set; }
        [Display(Name = "مالیات")]
        public float Tax { get; set; }
        [Display(Name = "حقوق")]
        public long Salary => CalculateSalary();

        // Store the user-defined formula (e.g., "Price * Days * (1 - Tax)")
        public string SalaryFormula { get; set; }

        private long CalculateSalary()
        {
            if (string.IsNullOrWhiteSpace(SalaryFormula))
                return 0;

            var expression = new NCalc.Expression(SalaryFormula);
            expression.Parameters["Price"] = Price;
            expression.Parameters["Days"] = Days;
            expression.Parameters["Tax"] = Tax;

            // Handle errors (e.g., invalid formula)
            try { return Convert.ToInt64(expression.Evaluate()); }
            catch { return 0; }
        }
    }
}