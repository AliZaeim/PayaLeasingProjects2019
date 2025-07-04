using Microsoft.Office.Interop.Word;
using System;
using System.Runtime.InteropServices;

namespace WordDocumentCompleting2019.Helpers
{
    public static class ConvertWordToPdfConverter
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
    }
}