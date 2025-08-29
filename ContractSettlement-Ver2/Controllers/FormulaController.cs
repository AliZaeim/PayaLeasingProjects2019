using ContractSettlement_Ver2.Data;
using ContractSettlement_Ver2.Models.Entites;
using ContractSettlement_Ver2.Models.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ContractSettlement_Ver2.Controllers
{
    public class FormulaController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Create()
        {
            var viewModel = new FormulaViewModel
            {
                AvailableFormulas = db.SalaryFormulas
                    .Select(f => new SelectListItem
                    {
                        Value = f.FormulaText,
                        Text = f.Title
                    }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(FormulaViewModel model)
        {
            model.AvailableFormulas = db.SalaryFormulas
                .Select(f => new SelectListItem
                {
                    Value = f.FormulaText,
                    Text = f.Title
                }).ToList();

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                model.Calculate();
                ViewBag.Message = "محاسبه با موفقیت انجام شد.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "خطا در محاسبه فرمول: " + ex.Message);
            }

            return View(model);
        }

        public ActionResult SaveFormula()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveFormula(SalaryFormula model)
        {
            if (!ModelState.IsValid)
                return View(model);

            db.SalaryFormulas.Add(model);
            db.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}