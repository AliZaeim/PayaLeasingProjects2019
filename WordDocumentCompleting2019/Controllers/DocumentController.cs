using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using WordDocumentCompleting2019.Helpers;
using WordDocumentCompleting2019.Models;

namespace WordDocumentCompleting2019.Controllers
{
    public class DocumentController : Controller
    {
        public ActionResult GenerateDocument()
        {
            // Define placeholders and values
            var placeholders = new Dictionary<string, string>
            {
                {"{{PazNo}}", "100500"},
                {"{{frDate}}", DateTime.Now.ToShortDateString()}
                // Add other placeholders
            };
            List<TemplateModel> MyplaceHolders = MakeTemplatesData();
                

            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/SalesLetterForm.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Document.docx");
        }
        private List<TemplateModel> MakeTemplatesData()
        {
            List<TemplateModel> templateModels = new List<TemplateModel>()
            {
                #region header
                new TemplateModel()
                    {
                        Key = "PazNo",
                        Value = "87452",
                        Group = "header"
                    },
                    new TemplateModel()
                    {
                        Key = "frDate",
                        Value = "1404/04/01",
                        Group = "header"
                    },
                    
	            #endregion header
                #region SellerInfo
                    new TemplateModel()
                    {
                        Key = "satan",
                        Value = "پایا اعتبار",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "SRELATED",
                        Value = "لیزینگ",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "stasis",
                        Value = "1400/01/01",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "SELLERID",
                        Value = "87965654",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "SADEREH",
                        Value = "تهران",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "SNC",
                        Value = "855236989",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Address",
                        Value = "تهران، سعادت آباد",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "ECONO",
                        Value = "852147",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "PHONE",
                        Value = "0212289625",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "CELLPHONE",
                        Value = "091214785214",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "POSTALCODE",
                        Value = "2225555888",
                        Group = "body"
                    },
                #endregion
                #region BuyerInfo
                    new TemplateModel()
                    {
                        Key = "BuyerName",
                        Value = "علی اصل زعیم",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BRELATED",
                        Value = "کرمعلی",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BTASIS",
                        Value = "1358/09/02",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BUYERID",
                        Value = "543",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "mahalesabt",
                        Value = "کرج",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BNC",
                        Value = "4899068549",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BADDRESS",
                        Value = "کرج، نظرآباد",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BECONO",
                        Value = "543489",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BPHONE",
                        Value = "02644632837",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BCELLPHONE",
                        Value = "09126617096",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BPOSTALCODE",
                        Value = "3335551898",
                        Group = "body"
                    },

                #endregion
                #region مورد_معامله
                    new TemplateModel()
                    {
                        Key = "Dastgah",
                        Value = "پژو پارس",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "SYSTEM",
                        Value = "XU7",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "TIP",
                        Value = "سال",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "MODEL",
                        Value = "1395",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "COLOR",
                        Value = "مشکی",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "ENGINENOMBER",
                        Value = "1478741258",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Shasi",
                        Value = "58ABC98YTR",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Shomare",
                        Value = "87-س-258-ایران 68",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Kedaraye",
                        Value = "دزدگیر",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "ORDERNUMBER",
                        Value = "ord1452",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Movarekh",
                        Value = "1404/04/05",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Company",
                        Value = "ایران خودرو",
                        Group = "body"
                    },
                #endregion مورد_معامله
                #region قیمت
                    new TemplateModel()
                    {
                        Key = "BahayeMoredMoeameleh",
                        Value = "8,000,000,000",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Forush",
                        Value = "5,000,000,000",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BeMojebe",
                        Value = "یک چک",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "MablaghBaghiMandeh",
                        Value = "3,000,000,000",
                        Group = "body"
                    },

                #endregion قیمت
                #region شرایط
                    new TemplateModel()
                    {
                        Key = "Shaba",
                        Value = "IR54548751648978465454541",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "FaseleyeRooz",
                        Value = "60",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "BeynameDate",
                        Value = "1404/04/04",
                        Group = "body"
                    },
                #endregion شرایط
                #region footer
                    new TemplateModel()
                    {
                        Key = "FooterSeller",
                        Value = "شرکت پایا لیزینگ",
                        Group = "footer"
                    },
                    new TemplateModel()
                    {
                        Key = "Kharidar",
                        Value = "علی اصل زعیم",
                        Group = "footer"
                    },
                    
	            #endregion footer
            };
            return templateModels;
        }

       
       

       

    }
}