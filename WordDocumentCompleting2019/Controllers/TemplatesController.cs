using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            //return Content("Ok");
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "CarInstallment.docx");
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
            //return Content("Ok");
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "CreditFacility.docx");
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
            
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "AttachmentNo1Installment.docx");
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
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "SalesLetterForm.docx");
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

            
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "VehicleDeliveryForm.docx");
        }
        
        

        
        //public byte[] ConvertWordToPdfFinall(byte[] wordBytes)
        //{
        //    string tempWordFile = null;
        //    string tempPdfFile = null;
        //    Application wordApp = null;
        //    Microsoft.Office.Interop.Word.Document doc = null;

        //    try
        //    {
        //        // Create temporary input file
        //        tempWordFile = Path.GetTempFileName();
        //        System.IO.File.WriteAllBytes(tempWordFile, wordBytes);

        //        // Create temporary PDF output path
        //        tempPdfFile = Path.ChangeExtension(Path.GetTempFileName(), ".pdf");

        //        // Initialize Word application
        //        wordApp = new Application
        //        {
        //            Visible = false,
        //            DisplayAlerts = WdAlertLevel.wdAlertsNone
        //        };

        //        // Open Word document
        //        doc = wordApp.Documents.Open(
        //            FileName: tempWordFile,
        //            ReadOnly: true,
        //            Visible: false
        //        );

        //        // Convert to PDF
        //        doc.SaveAs2(
        //            FileName: tempPdfFile,
        //            FileFormat: WdSaveFormat.wdFormatPDF
        //        );

        //        // Return PDF bytes
        //        return System.IO.File.ReadAllBytes(tempPdfFile);
        //    }
        //    finally
        //    {
        //        // Close document and release COM
        //        if (doc != null)
        //        {
        //            doc.Close(SaveChanges: false);
        //            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(doc);
        //            doc = null;
        //        }

        //        // Quit Word and release COM
        //        if (wordApp != null)
        //        {
        //            wordApp.Quit(SaveChanges: false);
        //            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wordApp);
        //            wordApp = null;
        //        }

        //        // Cleanup temporary files
        //        try
        //        {
        //            if (tempWordFile != null && System.IO.File.Exists(tempWordFile))
        //                System.IO.File.Delete(tempWordFile);
        //            if (tempPdfFile != null && System.IO.File.Exists(tempPdfFile))
        //                System.IO.File.Delete(tempPdfFile);
        //        }
        //        catch { /* Ignore file deletion errors */ }
        
        //        // Force cleanup of COM wrappers
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //        GC.Collect();
        //    }
        //}

    }
}