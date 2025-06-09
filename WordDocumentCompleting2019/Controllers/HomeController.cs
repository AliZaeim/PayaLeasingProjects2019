
using System.Web.Mvc;
using WordDocumentCompleting2019.Models;

namespace WordDocumentCompleting2019.Controllers
{
    // Controllers/HomeController.cs
    public class HomeController : Controller
    {
        private readonly WordDocumentService _docService = new WordDocumentService();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyTest1()
        {
            return View(new PersonModel());
        }
        [HttpPost]
        public ActionResult Preview(PersonModel model)
        {
            // Return to form if invalid
            if (!ModelState.IsValid) return View("Index", model);

            // Generate preview HTML
            ViewBag.PreviewContent = $"<p><strong>Name:</strong> {model.Name}</p>" +
                                    $"<p><strong>Family:</strong> {model.Family}</p>" +
                                    $"<p><strong>Age:</strong> {model.Age}</p>" +
                                    $"<p><strong>Father Name:</strong> {model.Father}</p>" +
                                    $"<p><strong>Job:</strong> {model.Job}</p>";

            // Store model in TempData for download
            TempData["DocumentModel"] = model;

            return View();
        }

        public ActionResult Download()
        {
            // Retrieve model from TempData
            var model = TempData["DocumentModel"] as PersonModel;
            if (model == null) return RedirectToAction("Index");

            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/PersonTemplate.docx");
            byte[] fileBytes = _docService.GenerateDocument(model, templatePath);

            return File(fileBytes,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                $"Profile_{model.Name}_{model.Family}.docx");
        }
    }
}