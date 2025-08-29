using ContractSettlement.Models;
using ContractSettlement.Models.Test;
using ContractSettlement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ContractSettlement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string userDefinedFormula = "(A + B + C)/ (C*3)";
            //MyClass myObj = new MyClass { A = 3, B = 4, C=2 };
            //myObj.CalculateResult(userDefinedFormula);
            //var r = myObj.Result;

            List<(string FieldName, Type FieldType, bool IsNullable)> result = NumericFieldScanner.ScanNumericFields(typeof(ContractSettlementModel)).ToList();
            var Infoes = result.Select(s =>new {name = s.FieldName,type = s.FieldType,nullable = s.IsNullable }).ToList();
            var res = result.Where(w => w.IsNullable).ToList();
            var tpes = res.Select(s => s.FieldType.Name).ToList();
            
            ContractSettlementModel contractSettlementModel = new ContractSettlementModel { FacilityAmount = 100, };
            return View();
        }
        public ActionResult ClassList()
        {
            var classes = ClassLister.GetClassNamesInNamespace("ContractSettlement.Models", includeSubNamespaces: false);
            return View();
        }
        public ActionResult ClsSettings()
        {
            return View();
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