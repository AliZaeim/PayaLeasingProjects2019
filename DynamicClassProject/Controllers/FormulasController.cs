using DynamicClassProject.Attributes;
using DynamicClassProject.Models;
using DynamicClassProject.Models.Data;
using DynamicClassProject.Models.Entities;
using DynamicClassProject.Models.ViewModels;
using DynamicClassProject.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DynamicClassProject.Controllers
{
    public class FormulasController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult GetNumericFieldsofClass(string className)
        {
            List<AppUtilities.PropertyInfoResult> data = AppUtilities.GetNumericProperties(className, false);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return Content(json, "application/json");
        }
        public ActionResult GetFormulaFieldsofClass(string className, int applyDb = 0, int getFormula = 0)
        {
            //applyDb
            //برای موقعی هستش که بخوای فیلدهای دارای فرمول در لیست فیلدها نمایش داده نشوند
            List<AppUtilities.PropInfo> FormulaFieldslist = AppUtilities.GetFormulaTargetPropertiesWithClassName(className);
            if (applyDb == 1)
            {
                List<Formula> FieldsThatHasinDb = db.Formulas.Where(w => w.ClassName == className).ToList();
                FormulaFieldslist = FormulaFieldslist
                    .Where(p => !FieldsThatHasinDb.Any(f => f.FieldName.Equals(p.PropName, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }
            if (getFormula == 1)
            {
                List<Formula> FieldsThatHasinDb = db.Formulas.Where(w => w.ClassName == className).ToList();
                if (FieldsThatHasinDb != null)
                {
                    foreach (var item in FormulaFieldslist)
                    {
                        item.PropFormula = FieldsThatHasinDb.FirstOrDefault(f => f.FieldName == item.PropName).Expression;
                    }

                }


            }

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
        /// <summary>
        /// فرم نمونه سازی از کلاس
        /// </summary>
        /// <returns></returns>
        public ActionResult Prototyping()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            List<string> clsNames = types.Where(w => Attribute.IsDefined(w, typeof(SpecialClassAttribute))).Select(s => s.Name).ToList();
            PrototypingViewModel prototypingViewModel = new PrototypingViewModel()
            {
                ClassNames = clsNames,
            };
            return View(prototypingViewModel);
        }
        [HttpPost]
        public ActionResult Prototyping(PrototypingViewModel prototypingViewModel)
        {
            // Get current assembly
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            Type clsType = types.FirstOrDefault(w => w.Name == prototypingViewModel.ClassName);
            var props = clsType.GetProperties();



            if (clsType == null)
            {
                return Content("Class not found: " + prototypingViewModel.ClassName);
            }
            var Properties = clsType.GetProperties()
                             .Select(p => new PropertyInput
                             {
                                 Name = p.Name,
                                 Type = p.PropertyType.Name,
                                 Value = ""  // empty for user input
                             }).ToList();
            if (!ModelState.IsValid) return View();
            

            return View();
        }
        
        public ActionResult CreateInstance(string className)
        {
            // Get current assembly
            var assembly = Assembly.GetExecutingAssembly();

            // Full namespace required: "MyApp.Models.Person"
            var type = assembly.GetType(className, throwOnError: false);

            if (type == null)
            {
                return Content("Class not found: " + className);
            }

            // Create instance
            object instance = Activator.CreateInstance(type);

            // Example: set some values (via reflection or binding later)
            foreach (var prop in type.GetProperties())
            {
                if (prop.CanWrite && prop.PropertyType == typeof(string))
                    prop.SetValue(instance, "Sample Value");
                else if (prop.CanWrite && prop.PropertyType == typeof(int))
                    prop.SetValue(instance, 123);
            }

            //return Json(instance, JsonRequestBehavior.AllowGet);
            return Content((string)instance, "application/json");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}