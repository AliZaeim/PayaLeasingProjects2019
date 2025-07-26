using Microsoft.Office.Interop.Word;
using MigraDoc.Rendering;
using System;
using System.Runtime.InteropServices;
using Xceed.Words.NET;

namespace WordDocumentCompleting2019.Helpers
{
    public static class WordToPdfConverter
    {
        public static bool Convert(string inputDocPath, string outputPdfPath)
        {
            Application wordApp = null;
            Document doc = null;

            try
            {
                // Initialize Word application (hidden)
                wordApp = new Application { Visible = false };

                // Open the Word document
                doc = wordApp.Documents.Open(inputDocPath);

                // Save as PDF (wdFormatPDF = 17)
                doc.SaveAs2(
                    FileName: outputPdfPath,
                    FileFormat: WdSaveFormat.wdFormatPDF
                );


                return true;
            }
            catch (Exception ex)
            {
                string m = $"Conversion failed: {ex.Message}";
                return false;
            }
            finally
            {
                // Cleanup COM objects to prevent memory leaks
                if (doc != null)
                {
                    doc.Close(false);
                    Marshal.ReleaseComObject(doc);
                }
                if (wordApp != null)
                {
                    wordApp.Quit(false);
                    Marshal.ReleaseComObject(wordApp);
                }
            }
        }
        public static bool ConvertDocxToPdf(string docxPath, string pdfPath)
        {
            bool result = false;
            Application wordApplication = new Application();
            try
            {
                Document wordDocument = wordApplication.Documents.Open(docxPath);
                wordDocument.ExportAsFixedFormat(
                    pdfPath,
                    WdExportFormat.wdExportFormatPDF,
                    OptimizeFor: WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Range: WdExportRange.wdExportAllDocument
                );
                wordDocument.Close(false);
                result = true;
            }
            catch (Exception ex) 
            {
                string Mess = ex.Message;
                result = false;
            }
            finally
            {
                wordApplication.Quit();
                
            }
            return result;
        }

        public static void ConvertDocxToPdf(string docxPath, string pdfPath)
        {
            // Read the DOCX file
            using (var document = DocX.Load(docxPath))
            {
                // Create MigraDoc document
                var migraDoc = new Document();
                var section = migraDoc.AddSection();

                // Add each paragraph from DOCX to MigraDoc
                foreach (var paragraph in document.Paragraphs)
                {
                    var migraParagraph = section.AddParagraph();
                    migraParagraph.AddText(paragraph.Text);

                    // You can add more formatting here if needed
                }

                // Render to PDF
                var renderer = new PdfDocumentRenderer();
                renderer.Document = migraDoc;
                renderer.RenderDocument();
                renderer.PdfDocument.Save(pdfPath);
            }
        }
    }

}