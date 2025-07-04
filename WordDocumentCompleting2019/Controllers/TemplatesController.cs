using Microsoft.Office.Interop.Word;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WordDocumentCompleting2019.Helpers;
using WordDocumentCompleting2019.Models;
namespace WordDocumentCompleting2019.Controllers
{
    public class TemplatesController : Controller
    {
        // GET: Templates
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// قرارداد تسهیلات فروش اقساطی خودرو 
        /// </summary>
        /// <returns></returns>
        public ActionResult CarInstallment() {
            List<TemplateModel> MyplaceHolders = CreateSampleData.CreateCarInstallmentData();


            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/CarInstallmentSaleFacilityAgreement.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            return GeneratePdf("CarInstallment.pdf", fileBytes);
            //return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "CarInstallment.docx");
        }
        /// <summary>
        /// پرسشنامه درخواست تسهیلات اعتباری
        /// </summary>
        /// <returns></returns>
        public ActionResult CreditFacility()
        {
            List<TemplateModel> MyplaceHolders = CreateSampleData.CreateCreditFacilityData();


            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/CreditFacilityApplicationQuestionnaire.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            return GeneratePdf("CreditFacility.pdf", fileBytes);
            //return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "CreditFacility.docx");
        }
        /// <summary>
        /// پیوست شماره یک قرارداد تسهیلات فروش اقساطی
        /// </summary>
        /// <returns></returns>
        public ActionResult AttachmentNoOne()
        {
            List<TemplateModel> MyplaceHolders = CreateSampleData.CreateAttachmentNo1Data();


            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/AttachmentNo1InstallmentSalesFacilityAgreement.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            return GeneratePdf("AttachmentNo1Installment.pdf", fileBytes);
            //return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "AttachmentNo1Installment.docx");
        }
        /// <summary>
        /// فرم بیعنامه
        /// </summary>
        /// <returns></returns>
        public ActionResult SalesLetterForm()
        {            
            List<TemplateModel> MyplaceHolders = CreateSampleData.CreateSalesFormData();
            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/SalesLetterForm.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);            
            
            string root = Server.MapPath("~/App_Data/DocumentTemplates/filename.PDF");
            return GeneratePdf(root,fileBytes);
            
            //return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "SalesLetterForm.docx");
        }
        
        //VehicleDeliveryandAcceptanceCertificateForm
        /// <summary>
        ///  فرم گواهی تحویل و قبول خودرو
        /// </summary>
        /// <returns></returns>
        public ActionResult VehicleDeliveryForm()
        {
            //VehicleDeliveryandAcceptanceCertificateForm
            List<TemplateModel> MyplaceHolders = CreateSampleData.CreateVehicleDeliveryaFormData();
            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/VehicleDeliveryandAcceptanceCertificateForm.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            
            
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "VehicleDeliveryForm.docx");
        }
        public ActionResult GoodsDeliveryForm()
        {
            List<TemplateModel> MyplaceHolders = CreateSampleData.CreateGoodDeliveryFormData();
            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/GoodsDeliveryAndConfirmationCertificateForm.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);

            //string virtualPath = "~/App_Data/GenerateWords";
            //string fileName = "GoodsDeliveryForm-filename.docx"; // Sanitize user-provided filenames!

            //string serverPath = Server.MapPath(virtualPath);
            //string fullPath = Path.Combine(serverPath, fileName);
            //using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            //{
            //    fs.Write(fileBytes, 0, fileBytes.Length);
            //}
            //string PdffileName = $"GoodsDeliveryForm_{DateTime.Now:yyyyMMdd-HHmmss}.pdf";
            //string PdfPath = Path.Combine("~/App_Data/Pdfs", PdffileName);
            //bool res = new WordToPdfConverter().Convert(fullPath, Server.MapPath(PdfPath));
            //if (res)
            //{
            //    if (System.IO.File.Exists(fullPath))
            //    {
            //        System.IO.File.Delete(fullPath);
            //    }
            //}
            //byte[] Pdffiledata = System.IO.File.ReadAllBytes(Server.MapPath(PdfPath));
            //string contentType = MimeMapping.GetMimeMapping(Server.MapPath(PdfPath));
            //return GeneratePdf("VehicleDeliveryForm.pdf", fileBytes);
            //return File(Pdffiledata, contentType);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "VehicleDeliveryForm.docx");
        }
        
        public ActionResult GeneratePdf(string root, byte[] WordBytes)
        {
            // 1. Create Word document in memory
            byte[] wordBytes = WordBytes;

            // 2. Save Word to temp file
            string tempDocx = Path.GetTempFileName() + ".docx";
            System.IO.File.WriteAllBytes(tempDocx, wordBytes);

            // 3. Convert to PDF using LibreOffice
            string tempPdf = Path.ChangeExtension(tempDocx, ".pdf");
            ConvertDocxToPdfUsingLibreOffice(tempDocx, tempPdf);

            // 4. Return PDF
            byte[] pdfBytes = System.IO.File.ReadAllBytes(tempPdf);
            System.IO.File.Delete(tempDocx);
            System.IO.File.Delete(tempPdf);

            return File(pdfBytes, "application/pdf", root);
        }
        private void ConvertDocxToPdfUsingLibreOffice(string inputPath, string outputPath)
        {
            string libreOfficePath = @"C:\Program Files\LibreOffice\program\soffice.exe";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = libreOfficePath,
                Arguments = $"--headless --convert-to pdf \"{inputPath}\" --outdir \"{Path.GetDirectoryName(outputPath)}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
                
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit(30000); // Timeout 30 seconds
            }
        }

        
        public byte[] ConvertWordToPdfFinall(byte[] wordBytes)
        {
            string tempWordFile = null;
            string tempPdfFile = null;
            Application wordApp = null;
            Microsoft.Office.Interop.Word.Document doc = null;

            try
            {
                // Create temporary input file
                tempWordFile = Path.GetTempFileName();
                System.IO.File.WriteAllBytes(tempWordFile, wordBytes);

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
                return System.IO.File.ReadAllBytes(tempPdfFile);
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
                    if (tempWordFile != null && System.IO.File.Exists(tempWordFile))
                        System.IO.File.Delete(tempWordFile);
                    if (tempPdfFile != null && System.IO.File.Exists(tempPdfFile))
                        System.IO.File.Delete(tempPdfFile);
                }
                catch { /* Ignore file deletion errors */ }

                // Force cleanup of COM wrappers
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

    }
}