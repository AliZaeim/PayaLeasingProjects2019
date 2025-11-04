using System.Web.Mvc;
using WordDocumentCompleting2019.Helpers;

namespace WordDocumentCompleting2019.Controllers
{
    // Controllers/HomeController.cs
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            string htmlContent = WordToHtmlConverterVer2.ConvertDocxToHtml(@"C:\Users\EZ-Tech\Downloads\forzaeim\forzaeim\Ejare.docx");
            WordToHtmlConverterVer2.SaveDocxAsHtml(@"C:\Users\EZ-Tech\Downloads\forzaeim\forzaeim\sample.html", htmlContent);
            return View();
        }
        
    }
}