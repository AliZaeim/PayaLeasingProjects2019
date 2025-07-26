using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace WordDocumentCompleting2019.Helpers
{
    public static class PdfTempService
    {
        public static void AddUnicodeCheckmark(string inputPdfPath, string outputPdfPath, int pageNumber, float x, float y)
        {
            using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create))
            {
                PdfReader reader = new PdfReader(inputPdfPath);
                using (PdfStamper stamper = new PdfStamper(reader, outputStream))
                {
                    PdfContentByte canvas = stamper.GetOverContent(pageNumber);

                    // Use a Unicode font (replace path if needed)
                    string fontPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\\webdings.ttf";
                    BaseFont font = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                    // Write check mark (U+2713 = ✓, U+2714 = ✔)
                    canvas.BeginText();
                    canvas.SetFontAndSize(font, 12);
                    canvas.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "\u2713", x, y, 0); // Or "\u2714"
                    canvas.EndText();
                }
            }
        }
        public static void FillPdfForm(string inputPdfPath,string outputPdfPath,Dictionary<string, dynamic> fieldValues, bool flattenForm = true)
        {
            // Validate inputs
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF not found", inputPdfPath);

            if (fieldValues == null || fieldValues.Count == 0)
                throw new ArgumentException("Field values dictionary is empty");

            PdfReader reader = null;
            FileStream outputStream = null;
            PdfStamper stamper = null;

            try
            {
                reader = new PdfReader(inputPdfPath);
                outputStream = new FileStream(outputPdfPath, FileMode.Create);
                stamper = new PdfStamper(reader, outputStream);
                
                // Get form fields
                AcroFields form = stamper.AcroFields;

                // Fill each field
                foreach (var entry in fieldValues)
                {
                    if (form.Fields.ContainsKey(entry.Key))
                    {
                        form.SetField(entry.Key, entry.Value);
                    }
                    else
                    {
                        // Optional: Log missing fields
                        Console.WriteLine($"Field '{entry.Key}' not found in PDF");
                    }
                }

                // Flatten to remove form fields (make non-editable)
                stamper.FormFlattening = flattenForm;
            }
            finally
            {
                // Cleanup resources
                stamper?.Close();
                reader?.Close();
                outputStream?.Close();
            }
        }

        public static void FillFormPdf(string pdfTemplatePath, string outputPdfPath, Dictionary<string, string> formData)
        {
            using (PdfReader reader = new PdfReader(pdfTemplatePath))
            using (FileStream fs = new FileStream(outputPdfPath, FileMode.Create))
            {
                PdfStamper stamper = new PdfStamper(reader, fs);
                AcroFields fields = stamper.AcroFields;

                foreach (var item in formData)
                    fields.SetField(item.Key, item.Value); // e.g., ("FirstName", "John")

                stamper.FormFlattening = true; // Lock form after filling
                stamper.Close();
            }
        }
        public static void FillPdfForm(string sourcePdfPath,string destinationPdfPath,Dictionary<string, string> fieldValues)
        {
            using (var reader = new PdfReader(sourcePdfPath))
            using (var outputStream = new FileStream(destinationPdfPath, FileMode.Create))
            {
                var stamper = new PdfStamper(reader, outputStream);
                var formFields = stamper.AcroFields;

                // Set values for each field
                foreach (var entry in fieldValues)
                {
                    formFields.SetField(entry.Key, entry.Value);
                }

                // Flatten to make fields non-editable (remove form)
                stamper.FormFlattening = true;
                stamper.Close();
            }
        }

        public static void FillPdfTemplate(string inputPath, string outputPath)
        {
            // Load Unicode font for RTL characters (e.g., Arial)
            string fontPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            using (PdfReader reader = new PdfReader(inputPath))
            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            {
                using (PdfStamper stamper = new PdfStamper(reader, fs))
                {
                    AcroFields fields = stamper.AcroFields;

                    // Set values and RTL properties for each field
                    fields.SetField("Field1", "نص عربي تجريبي"); // Arabic text
                    fields.SetFieldProperty("Field1", "textfont", bf, null);
                    fields.SetFieldProperty("Field1", "setflags", PdfFormField.Q_RIGHT, null);

                    fields.SetField("Field2", "טקסט בעברית"); // Hebrew text
                    fields.SetFieldProperty("Field2", "textfont", bf, null);
                    fields.SetFieldProperty("Field2", "setflags", PdfFormField.FF_RICHTEXT, null);

                    // Flatten to lock fields (remove interactivity)
                    stamper.FormFlattening = true;
                }
            }
        }

        public static List<string> ExtractEnglishWords(string pdfPath)
        {
            List<string> words = new List<string>();
            // Regex for English words (allows apostrophes/hyphens internally)
            Regex wordRegex = new Regex(@"\b[a-zA-Z]+(?:['-][a-zA-Z]+)*\b", RegexOptions.Compiled);

            using (PdfReader reader = new PdfReader(pdfPath))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(
                        reader,
                        page,
                        new LocationTextExtractionStrategy()
                    );

                    // Handle hyphenated line breaks
                    pageText = pageText.Replace("-\r\n", "").Replace("-\n", "");

                    // Extract matching words
                    foreach (Match match in wordRegex.Matches(pageText))
                    {
                        words.Add(match.Value);
                    }
                }
            }
            return words;
        }
        public static void FillPdfFile(string inputPdfPath, string outputPdfPath, Dictionary<string, string> fieldValues)
        {
            // Validate inputs
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF not found", inputPdfPath);

            if (fieldValues == null || fieldValues.Count == 0)
                throw new ArgumentException("Field values dictionary is empty");
            PdfReader reader = null;
            FileStream outputStream = null;
            PdfStamper stamper = null;

            try
            {
                reader = new PdfReader(inputPdfPath);
                outputStream = new FileStream(outputPdfPath, FileMode.Create);
                stamper = new PdfStamper(reader, outputStream);

                // Get form fields
                AcroFields form = stamper.AcroFields;
                
                // Fill each field
                foreach (var entry in fieldValues)
                {
                    
                    if (form.Fields.ContainsKey(entry.Key))
                    {
                        form.SetField(entry.Key, entry.Value);
                    }
                    else
                    {
                        // Optional: Log missing fields
                        Console.WriteLine($"Field '{entry.Key}' not found in PDF");
                    }
                }

                // Flatten to remove form fields (make non-editable)
                stamper.FormFlattening = true;
            }
            finally
            {
                // Cleanup resources
                stamper?.Close();
                reader?.Close();
                outputStream?.Close();
            }

        }


    }
}
