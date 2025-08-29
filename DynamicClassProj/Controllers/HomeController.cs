using DynamicClassProj.Models.Data;
using DynamicClassProj.Models.Entities;
using DynamicClassProj.Models.Test;
using DynamicClassProj.Utilities;
using DynamicClassProj.Utilities.Test;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace DynamicClassProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            List<Formula> lst = db.Formulas.Where(w => w.ClassName=="TestModel").ToList();// Get from db
            var formulas = new List<Formula> // Static
            {
                new Formula { FieldName = "C", Expression = "Pow(A,B)" },
                new Formula { FieldName = "D", Expression = "A * (B + C)" }
            };

            var model = new TestModel { A = 2, B = 3 };
            model.Calculate(lst);

            TestModel testModel = new TestModel();
            
            FormulaEvaluator formulaEvaluator = new FormulaEvaluator(lst);
            
            var res = formulaEvaluator.Evaluate(2, 3);
            

            var types = Assembly.GetExecutingAssembly().GetTypes();
            var tt = types.Where(w => Attribute.IsDefined(w, typeof(SpecialClassAttribute))).Select(s => s.Name).ToList();
            var classes = ClassLister.GetClassNamesInNamespace("DynamicClassProj.Models", includeSubNamespaces: true);
            var numericFields = NumericPropertyInspector.GetNumericProperties(typeof(ContractSettlement));
            var FormulaFieldslist = FormulaTargetHelper.GetFormulaTargetPropertyNames<ContractSettlement>();
           
            //return View();
            //List<(string className, string Formula, string FormulaName)> formulas = new List<(string, string, string)>()
            //{
            //    ("MyModel","A*B+3","RF1"),
            //    ("ZModel","Round(Pow((A/B),2.5) * 3.2,2)","RF2"),
            //    ("ZClass","Sin(90)*3.45","RF3"),
            //    ("MyModel","Pow(10,5)","RF4")

            //};

            //MyModel model = new MyModel()
            //{
            //    A = 4,
            //    B = 2,
            //    ResultFormula = formulas.FirstOrDefault(f => f.className == "ZModel" && f.FormulaName == "RF2").Formula,
            //    ResultFormula2 = formulas.LastOrDefault(f => f.className == "MyModel" && f.FormulaName == "RF4").Formula,
            //};

            //FormulaEvaluator.EvaluateFormulasWithNCalc(model);
            //return Json(model, JsonRequestBehavior.AllowGet);
            return View();
        }

        private static void TestMethod()
        {
            var properties = TypeDescriptor.GetProperties(typeof(MyClass));
            foreach (PropertyDescriptor prop in properties)
            {
                var displayAttr = prop.Attributes[typeof(DynamicDisplayNameAttribute)] as DynamicDisplayNameAttribute;
                if (displayAttr != null)
                    Console.WriteLine($"{prop.Name}: DisplayName = {displayAttr.DisplayName}");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}