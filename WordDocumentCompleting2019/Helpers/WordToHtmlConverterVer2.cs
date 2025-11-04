using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using System.IO;

namespace WordDocumentCompleting2019.Helpers
{
    public static class WordToHtmlConverterVer2
    {
        public static string ConvertDocxToHtml(string docxPath)
        {
            if (!File.Exists(docxPath))
                throw new FileNotFoundException("Word file not found", docxPath);

            using (FileStream fs = new FileStream(docxPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (WordprocessingDocument wDoc = WordprocessingDocument.Open(fs, false))
            {
                var settings = new HtmlConverterSettings()
                {
                    PageTitle = Path.GetFileNameWithoutExtension(docxPath),
                    FabricateCssClasses = true,
                    CssClassPrefix = "docx",   // optional: avoids class name conflicts
                    RestrictToSupportedLanguages = false,
                    RestrictToSupportedNumberingFormats = false
                };

                // Convert Word document to Html
                var html = HtmlConverter.ConvertToHtml(wDoc, settings);

                return html.ToString();  // ready-to-use HTML
            }
        }

        /// <summary>
        /// Converts a DOCX file to a standalone HTML file.
        /// </summary>
        public static void SaveDocxAsHtml(string docxPath, string htmlPath)
        {
            string html = ConvertDocxToHtml(docxPath);
            File.WriteAllText(htmlPath, html);
        }
    }
}