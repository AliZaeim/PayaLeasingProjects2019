using ContractSettlement.Models.ViewModels;
using System;
using System.Web.Mvc;

namespace ContractSettlement.Controllers
{
    public class SalaryController : Controller
    {
        // GET: Show formula input form
        public ActionResult Calculate()
        {
            return View(new FormulaViewModel());
        }

        // POST: Calculate salary using formula
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(FormulaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CalculatedSalary = EvaluateSalaryFormula(model.SalaryFormula,model.Price, model.Days,model.Tax);
                    FormulaViewModel formulaViewModel = new FormulaViewModel();

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("SalaryFormula", $"خطا در محاسبه: {ex.Message}");
                }
            }
            return View(model);
        }

        private long EvaluateSalaryFormula(string formula, int price, int days, float tax)
        {
            var expr = new NCalc.Expression(formula);
            expr.Parameters["Price"] = price;
            expr.Parameters["Days"] = days;
            expr.Parameters["Tax"] = tax;

            expr.EvaluateFunction += (name, args) =>
                throw new InvalidOperationException($"تابع '{name}' پشتیبانی نمی‌شود");

            var result = expr.Evaluate();
            return Convert.ToInt64(result);
        }
    }
}