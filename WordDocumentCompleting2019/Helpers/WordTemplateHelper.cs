using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
using OpenXmlPowerTools;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using WordDocumentCompleting2019.Models;

namespace WordDocumentCompleting2019.Helpers
{
    public static class WordTemplateHelper
    {
        public static void ReplacePlaceholders(string filePath, Dictionary<string, string> placeholders)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, true))
            {
                // Replace in main document body
                ReplaceInElement(doc.MainDocumentPart.Document.Body, placeholders);

                // Replace in headers
                foreach (var headerPart in doc.MainDocumentPart.HeaderParts)
                    ReplaceInElement(headerPart.Header, placeholders);

                // Replace in footers
                foreach (var footerPart in doc.MainDocumentPart.FooterParts)
                    ReplaceInElement(footerPart.Footer, placeholders);
            }
        }
        public static void ReplacePlaceholders(string filePath, List<TemplateModel> placeholders)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, true))
            {
                MarkupSimplifier.SimplifyMarkup(doc, settings: new SimplifyMarkupSettings
                {
                    RemoveComments = true,
                    RemoveContentControls = true,
                    RemoveEndAndFootNotes = false,
                    RemoveHyperlinks = true,                     
                });
                // Replace in headers
                foreach (var headerPart in doc.MainDocumentPart.HeaderParts.Where(w => !string.IsNullOrEmpty(w.Header.InnerText)).ToList())
                    ReplaceInElement(headerPart.Header, placeholders.Where(w => w.Group == "header").ToList());

                // Replace in main document body
                ReplaceInElement(doc.MainDocumentPart.Document.Body, placeholders.Where(w => w.Group == "body").ToList());

                

                // Replace in footers
                foreach (FooterPart footerPart in doc.MainDocumentPart.FooterParts.Where(w => !string.IsNullOrEmpty(w.Footer.InnerText)).ToList())
                    ReplaceInElement(footerPart.Footer, placeholders.Where(w => w.Group == "footer").ToList());
            }
        }

        private static void ReplaceInElement(OpenXmlElement element, Dictionary<string, string> placeholders)
        {
            foreach (var text in element.Descendants<Text>())
            {
                foreach (var kvp in placeholders)
                {
                    // Case-insensitive replacement with regex
                    text.Text = Regex.Replace(
                        text.Text,
                        Regex.Escape(kvp.Key),
                        HttpUtility.HtmlEncode(kvp.Value), // Escape XML chars
                        RegexOptions.IgnoreCase
                    );
                }
            }
        }
        private static void ReplaceInElement(OpenXmlElement element, List<TemplateModel> placeholders)
        {    
            if (!string.IsNullOrWhiteSpace(element.InnerText))
            {            
                var txts = element.Descendants<Text>().ToList();
                txts = txts.Where(w => !string.IsNullOrWhiteSpace(w.Text)).ToList();
                txts = txts.Where(s => s.Text.All(c => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))).ToList();
                txts = txts.DistinctBy(d => d.Text).ToList();
                foreach (var ph in placeholders)
                {
                    if (txts.Select(s => s.Text.Trim()).ToList().Exists(a => a == ph.Key.Trim()))
                    {
                        Text txt = txts.FirstOrDefault(f => f.Text.Trim().Equals(ph.Key.Trim()));
                        txt.Text = Regex.Replace(
                                    txt.Text,
                                    Regex.Escape(ph.Key),
                                    HttpUtility.HtmlEncode(ph.Value), // Escape XML chars
                                    RegexOptions.IgnoreCase
                                );
                        //txt.Text = ph.Value;
                    }
                }
            }
            
            
           
            
        }
        
    }
}