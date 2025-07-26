using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WordDocumentCompleting2019.Helpers;
using WordDocumentCompleting2019.Models;

namespace WordDocumentCompleting2019.Controllers
{
    public class DefaultController : Controller
    {
        /// <summary>
        /// قرارداد تسهیلات فروش اقساطی خودرو 
        /// </summary>
        /// <returns></returns>
        public ActionResult CarInstallment()
        {
            List<TemplateModel> MyplaceHolders = CreateSampleData.CreateCarInstallmentData();
            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/CarInstallmentSaleFacilityAgreement.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);

            string virtualPath = "~/App_Data/GenerateWords";
            string fileName = "CarInstallment-filename.docx"; // Sanitize user-provided filenames!
            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
            string PdfVirtualpath = "~/App_Data/Pdfs";
            string PdffileName = $"CarInstallment{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfserverPath = Server.MapPath(PdfVirtualpath);
            string PdfPath = Path.Combine(PdfserverPath, PdffileName);


            string htmlVirtualpath = "~/App_Data/htmls";
            string htmlfileName = $"CarInstallment{DateTime.Now:yyyyMMddTHHmmss}.html";
            string htmlserverPath = Server.MapPath(htmlVirtualpath);
            string htmlPath = Path.Combine(htmlserverPath, htmlfileName);
            //System.IO.File.WriteAllText(Path.Combine(PdfserverPath, $"test{DateTime.Now:yyyyMMddTHHmmss}.txt"), "test");
            WordToHtmlConverter.Convert(fullPath, htmlPath);

            return Content("با موفقیت ایجاد شد !");
                      
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
            string virtualPath = "~/App_Data/GenerateWords";
            string fileName = "CreditFacility-filename.docx"; // Sanitize user-provided filenames!

            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
            string PdffileName = $"CreditFacility{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfPath = Path.Combine("~/App_Data/Pdfs", PdffileName);
            bool res = WordToPdfService.Convert(fullPath, Server.MapPath(PdfPath));
            if (res)
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(Server.MapPath(PdfPath));
            string contentType = MimeMapping.GetMimeMapping(Server.MapPath(PdfPath));
            return File(Pdffiledata, contentType, PdffileName);
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
            string virtualPath = "~/App_Data/GenerateWords";
            string fileName = "AttachmentNo1Installment-filename.docx"; // Sanitize user-provided filenames!

            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
            string PdffileName = $"AttachmentNo1Installment{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfPath = Path.Combine("~/App_Data/Pdfs", PdffileName);
            bool res = WordToPdfService.Convert(fullPath, Server.MapPath(PdfPath));
            if (res)
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(Server.MapPath(PdfPath));
            string contentType = MimeMapping.GetMimeMapping(Server.MapPath(PdfPath));
            return File(Pdffiledata, contentType, PdffileName);
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

            string virtualPath = "~/App_Data/GenerateWords";
            string fileName = "SalesLetterForm-filename.docx"; // Sanitize user-provided filenames!

            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
            string PdffileName = $"SalesLetterForm{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfPath = Path.Combine("~/App_Data/Pdfs", PdffileName);
            bool res = WordToPdfService.Convert(fullPath, Server.MapPath(PdfPath));
            if (res)
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(Server.MapPath(PdfPath));
            string contentType = MimeMapping.GetMimeMapping(Server.MapPath(PdfPath));
            return File(Pdffiledata, contentType, PdffileName);
        }
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
            string virtualPath = "~/App_Data/GenerateWords";
            string fileName = "VehicleDeliveryaForm-filename.docx"; // Sanitize user-provided filenames!

            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
            string PdffileName = $"VehicleDeliveryaForm{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfPath = Path.Combine("~/App_Data/Pdfs", PdffileName);
            bool res = WordToPdfService.Convert(fullPath, Server.MapPath(PdfPath));
            if (res)
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(Server.MapPath(PdfPath));
            string contentType = MimeMapping.GetMimeMapping(Server.MapPath(PdfPath));
            return File(Pdffiledata, contentType, PdffileName);
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

            string virtualPath = "~/App_Data/GenerateWords";
            string fileName = "GoodsDeliveryForm-filename.docx"; // Sanitize user-provided filenames!

            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
            string PdffileName = $"GoodsDeliveryForm_{DateTime.Now:yyyyMMdd-HHmmss}.pdf";
            string PdfPath = Path.Combine("~/App_Data/Pdfs", PdffileName);
            bool res = WordToPdfService.Convert(fullPath, Server.MapPath(PdfPath));
            if (res)
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(Server.MapPath(PdfPath));
            string contentType = MimeMapping.GetMimeMapping(Server.MapPath(PdfPath));
            return File(Pdffiledata, contentType, PdffileName);

        }
    }
}