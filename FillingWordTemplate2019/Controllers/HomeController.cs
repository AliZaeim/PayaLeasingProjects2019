using System.IO;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using FillingWordTemplate2019.Models;
using System;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;
using DocumentFormat.OpenXml;

namespace FillingWordTemplate2019.Controllers
{
    // Controllers/HomeController.cs
    

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new UserData());
        }

        [HttpPost]
        public ActionResult Generate(UserData model)
        {
            string templatePath = Server.MapPath("~/App_Data/Template.docx");
            string outputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.docx");
            System.IO.File.Copy(templatePath, outputPath);

            using (WordprocessingDocument doc = WordprocessingDocument.Open(outputPath, true))
            {
                // Replace with unique placeholders
                ReplacePlaceholder(doc, "{{NAME}}", model.Name);
                ReplacePlaceholder(doc, "{{FAMILY}}", model.Family);
                ReplacePlaceholder(doc, "{{AGE}}", model.Age);
            }

            Session["GeneratedFilePath"] = outputPath;
            return RedirectToAction("Preview");
        }

        private void ReplacePlaceholder(WordprocessingDocument doc, string placeholder, string value)
        {
            var body = doc.MainDocumentPart.Document.Body;
            foreach (var text in body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>())
            {
                if (text.Text.Contains(placeholder))
                {
                    text.Text = text.Text.Replace(placeholder, value);
                }
            }
        }

        public ActionResult Preview()
        {
            string filePath = Session["GeneratedFilePath"] as string;
            if (!System.IO.File.Exists(filePath))
                return HttpNotFound();

            string htmlContent = ConvertToHtml(filePath);
            return View((object)htmlContent);
        }

        private string ConvertToHtml(string docPath)
        {
            byte[] byteArray = System.IO.File.ReadAllBytes(docPath);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(byteArray, 0, byteArray.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
                {
                    HtmlConverterSettings settings = new HtmlConverterSettings()
                    {
                        PageTitle = "Document Preview"
                    };

                    string fullHtml = HtmlConverter.ConvertToHtml(doc, settings);

                    // Extract only the body content
                    int bodyStart = fullHtml.IndexOf("<body>") + 6;
                    int bodyEnd = fullHtml.IndexOf("</body>");
                    if (bodyStart >= 6 && bodyEnd > bodyStart)
                    {
                        return fullHtml.Substring(bodyStart, bodyEnd - bodyStart);
                    }

                    return fullHtml; // Fallback
                }
            }
        }
        private void ReplaceBookmark(WordprocessingDocument doc, string bookmarkName, string value)
        {
            var bookmarks = doc.MainDocumentPart.RootElement
                .Descendants<BookmarkStart>()
                .Where(b => b.Name == bookmarkName);

            foreach (var bookmark in bookmarks)
            {
                OpenXmlElement parent = bookmark.Parent;
                while (parent != null && !(parent is Run))
                {
                    parent = parent.Parent;
                }

                if (parent != null)
                {
                    parent.RemoveAllChildren<Text>();
                    parent.AppendChild(new Text(value));
                }
            }
        }
        public ActionResult Download()
        {
            string filePath = Session["GeneratedFilePath"] as string;
            if (!System.IO.File.Exists(filePath))
                return HttpNotFound();

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath); // Cleanup
            Session.Remove("GeneratedFilePath"); // Clear session
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Document.docx");
        }
    }
}