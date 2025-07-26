using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace WordDocumentCompleting2019.Helpers
{
    public static class WordToPdfService
    {
        public static byte[] ConvertWordToPdf(byte[] wordBytes)
        {
            
            string tempWordFile = null;
            string tempPdfFile = null;
            Application wordApp = null;
            Document doc = null;

            try
            {
                // Create temporary input file
                tempWordFile = Path.GetTempFileName();
                File.WriteAllBytes(tempWordFile, wordBytes);

                // Create temporary PDF output path
                tempPdfFile = Path.ChangeExtension(Path.GetTempFileName(), ".pdf");

                // Initialize Word application
                wordApp = new Application
                {
                    Visible = false,
                    DisplayAlerts = WdAlertLevel.wdAlertsNone
                };

                // Open Word document
                doc = wordApp.Documents.Open(
                    FileName: tempWordFile,
                    ReadOnly: true,
                    Visible: false
                );

                // Convert to PDF
                doc.SaveAs2(
                    FileName: tempPdfFile,
                    FileFormat: WdSaveFormat.wdFormatPDF
                );

                // Return PDF bytes
                return File.ReadAllBytes(tempPdfFile);
            }
            finally
            {
                // Close document and release COM
                if (doc != null)
                {
                    doc.Close(SaveChanges: false);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(doc);
                    doc = null;
                }

                // Quit Word and release COM
                if (wordApp != null)
                {
                    wordApp.Quit(SaveChanges: false);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wordApp);
                    wordApp = null;
                }

                // Cleanup temporary files
                try
                {
                    if (tempWordFile != null && File.Exists(tempWordFile))
                        File.Delete(tempWordFile);
                    if (tempPdfFile != null && File.Exists(tempPdfFile))
                        File.Delete(tempPdfFile);
                }
                catch { /* Ignore file deletion errors */ }

                // Force cleanup of COM wrappers
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
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