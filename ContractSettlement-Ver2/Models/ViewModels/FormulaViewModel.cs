using NCalc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContractSettlement_Ver2.Models.ViewModels
{
    public class FormulaViewModel
    {
        public int Price { get; set; }
        public int Days { get; set; }
        public float Tax { get; set; }

        [Required]
        [Display(Name = "فرمول انتخاب شده")]
        public string SalaryFormula { get; set; }

        public long CalculatedSalary { get; set; }

        public List<SelectListItem> AvailableFormulas { get; set; } = new List<SelectListItem>();

        public void Calculate()
        {
            var expr = new Expression(SalaryFormula);
            expr.Parameters["Price"] = Price;
            expr.Parameters["Days"] = Days;
            expr.Parameters["Tax"] = Tax;

            var result = expr.Evaluate();
            CalculatedSalary = System.Convert.ToInt64(result);
        }
    }
}