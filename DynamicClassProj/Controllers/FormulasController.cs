using DynamicClassProj.Models.Data;
using DynamicClassProj.Models.Test;
using DynamicClassProj.Models.ViewModels;
using DynamicClassProj.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DynamicClassProj.Controllers
{
    public class FormulasController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult GetNumericFieldsofClass(string className)
        {
            List<NumericPropertyInspector.PropertyInfoResult> data =  NumericPropertyInspector.GetNumericProperties(className,false);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return Content(json, "application/json");
        }
        public ActionResult GetFormulaFieldsofClass(string className)
        {
            List<FormulaTargetHelper.PropInfo> FormulaFieldslist = FormulaTargetHelper.GetFormulaTargetPropertiesWithClassName(className);
            List<Formula> FieldsThatHasinDb = db.Formulas.Where(w => w.ClassName == className).ToList();
            FormulaFieldslist = FormulaFieldslist
                .Where(p => !FieldsThatHasinDb.Any(f => f.FieldName.Equals(p.PropName, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(FormulaFieldslist);
            return Content(json, "application/json");
        }
        // GET: Formulas
        public ActionResult Index()
        {
            return View(db.Formulas.OrderBy(f => f.Id).ToList());
        }
        // GET: Formulas/Create
        public ActionResult Create()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            List<string> clsNames = types.Where(w => Attribute.IsDefined(w, typeof(SpecialClassAttribute))).Select(s => s.Name).ToList();
            FormulaDefinitionViewModel formulaDefinitionViewModel = new FormulaDefinitionViewModel()
            {
                ClassNames = clsNames.ToList(),
            };
            return View(formulaDefinitionViewModel);
        }
        // POST: Formulas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormulaDefinitionViewModel formulaDefinitionViewModel)
        {
            if (ModelState.IsValid)
            {
                Formula formula = new Formula()
                {
                    ClassName = formulaDefinitionViewModel.ClassName,
                    FieldName = formulaDefinitionViewModel.FieldName,
                    Expression = formulaDefinitionViewModel.FormulaText,
                    Description = formulaDefinitionViewModel.Description,
                    
                };
                
                db.Formulas.Add(formula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formulaDefinitionViewModel);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Formula formula = db.Formulas.FirstOrDefault(f => f.Id == id);
            if (formula == null)
            {
                return HttpNotFound();
            }
            return View(formula);
        }
        // GET: Formulas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var formula = db.Formulas.Find(id);
            if (formula == null) return HttpNotFound();
            return View(formula);
        }
        // POST: Formulas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Formula formula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formula);
        }
        // GET: Formulas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var formula = db.Formulas.Find(id);
            if (formula == null) return HttpNotFound();
            return View(formula);
        }
        // POST: Formulas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var formula = db.Formulas.Find(id);
            db.Formulas.Remove(formula);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}