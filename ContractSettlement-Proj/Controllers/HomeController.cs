using ContractSettlement_Proj.Models.Entities;
using ContractSettlement_Proj.Utilities;
using System.Collections.Generic;
using System.Web.Mvc;
using static ContractSettlement_Proj.Utilities.NumericPropertyInspector;

namespace ContractSettlement_Proj.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var targetFields = FormulaTargetHelper.GetFormulaTargetPropertyNames<ContractSettlement>();
            List<PropertyInfoResult> numericFields = NumericPropertyInspector.GetNumericProperties(typeof(ContractSettlement));
            var classes = ClassScanner.GetClassNamesFromProject("ContractSettlement-Proj.Models");
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