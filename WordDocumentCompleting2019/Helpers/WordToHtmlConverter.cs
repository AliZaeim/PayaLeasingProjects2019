using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WordDocumentCompleting2019.Helpers
{
    public static class WordToHtmlConverter
    {
        public static void Convert(string wordPath, string htmlPath)
        {
            // Read the Word document into a byte array
            byte[] byteArray = File.ReadAllBytes(wordPath);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(byteArray, 0, byteArray.Length);

                // Open the Word document
                using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
                {
                    // Convert to HTML using OpenXmlPowerTools
                    HtmlConverterSettings settings = new HtmlConverterSettings();
                    string html = (string)HtmlConverter.ConvertToHtml(doc, settings);

                    // Save HTML to file
                    File.WriteAllText(htmlPath, html);
                }
            }
        }
    }
}