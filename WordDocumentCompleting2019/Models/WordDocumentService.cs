using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordDocumentCompleting2019.Models
{
    public class WordDocumentService
    {
        public byte[] GenerateDocument(PersonModel model, string templatePath)
        {
            byte[] templateBytes = File.ReadAllBytes(templatePath);
            using (var memStream = new MemoryStream())
            {
                memStream.Write(templateBytes, 0, templateBytes.Length);
                memStream.Position = 0;

                using (var doc = WordprocessingDocument.Open(memStream, true))
                {
                    // Process main body
                    ProcessElement(doc.MainDocumentPart.Document.Body, model);

                    // Process headers
                    foreach (var header in doc.MainDocumentPart.HeaderParts)
                    {
                        ProcessElement(header.Header, model);
                    }

                    // Process footers
                    foreach (var footer in doc.MainDocumentPart.FooterParts)
                    {
                        ProcessElement(footer.Footer, model);
                    }

                    // Process footnotes
                    if (doc.MainDocumentPart.FootnotesPart != null)
                    {
                        ProcessElement(doc.MainDocumentPart.FootnotesPart.Footnotes, model);
                    }
                }
                return memStream.ToArray();
            }
        }

        private void ProcessElement(OpenXmlElement element, PersonModel model)
        {
            var placeholders = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"{{Name}}", model.Name},
            {"{{Family}}", model.Family},
            {"{{Age}}", model.Age.ToString()},
            {"{{Father}}", model.Father},
            {"{{Job}}", model.Job}
        };

            // First pass: Direct replacements
            foreach (var text in element.Descendants<Text>())
            {
                foreach (var ph in placeholders)
                {
                    if (text.Text.Contains(ph.Key))
                    {
                        text.Text = text.Text.Replace(ph.Key, ph.Value);
                    }
                }
            }

            // Second pass: Combine split runs
            foreach (var run in element.Descendants<Run>())
            {
                var texts = run.Descendants<Text>().ToList();
                if (texts.Count <= 1) continue;

                string combined = string.Concat(texts.Select(t => t.Text));
                if (placeholders.Keys.Any(combined.Contains))
                {
                    foreach (var ph in placeholders)
                    {
                        combined = combined.Replace(ph.Key, ph.Value);
                    }

                    // Clear existing text elements
                    foreach (var text in texts)
                    {
                        text.Remove();
                    }

                    // Add new combined text
                    run.AppendChild(new Text(combined));
                }
            }
        }
    }

}