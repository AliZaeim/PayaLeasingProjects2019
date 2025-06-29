using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            List<TemplateModel> MyplaceHolders = CreateCarInstallmentData();


            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/CarInstallmentSaleFacilityAgreement.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "CarInstallment.docx");
        }
        /// <summary>
        /// پرسشنامه درخواست تسهیلات اعتباری
        /// </summary>
        /// <returns></returns>
        public ActionResult CreditFacility()
        {
            List<TemplateModel> MyplaceHolders = CreateCreditFacilityData();


            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/CreditFacilityApplicationQuestionnaire.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "CreditFacility.docx");
        }
        private List<TemplateModel> CreateCarInstallmentData()
        {
            List<TemplateModel> templateModels = new List<TemplateModel>()
            {
                #region header
                    new TemplateModel()
                    {
                        Key = "agNumber",
                        Value = "404002",
                        Group = "header"
                    },
                    new TemplateModel()
                    {
                        Key = "contDate",
                        Value = "1404/02/25",
                        Group = "header"
                    },
                #endregion
                #region body
                    #region خریدار
                        new TemplateModel()
                        {
                            Key = "BuyerName",
                            Value = "علی اصل زعیم",
                            Group = "body"
                        },
                        new TemplateModel()
                        {
                            Key = "BFatherName",
                            Value = "کرمعلی",
                            Group = "body"
                        },
                        new TemplateModel()
                        {
                            Key = "BTarikhT",
                            Value = "1358/09/02",
                            Group = "body"
                        },
                        new TemplateModel()
                        {
                            Key = "BIdNo",
                            Value = "543",
                            Group = "body"
                        },
                        new TemplateModel()
                        {
                            Key = "IssuedBy",
                            Value = "کــرج",
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
                            Key = "BAddress",
                            Value = "کرج، نظرآباد",
                            Group = "body"
                        },
                        new TemplateModel()
                        {
                            Key = "BPhone",
                            Value = "02644632837",
                            Group = "body"
                        },
                        new TemplateModel()
                        {
                            Key = "BCellphone",
                            Value = "09126617096",
                            Group = "body"
                        },
                #endregion
                    #region ضامنین
                        #region Zamen1
                            new TemplateModel()
                            {
                                Key = "ZamenOne",
                                Value = "علی عباسی",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZFarzandOne",
                                Value = "محمود",
                                Group = "body"
                            },                            
                            new TemplateModel()
                            {
                                Key = "ZStartDateOne",
                                Value = "1402/11/12",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZIdNumberOne",
                                Value = "8554796",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZIssuedByOne",
                                Value = "تهران",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZNCOne",
                                Value = "0015428965",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZPHONE",
                                Value = "02144567895",
                                Group= "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZAddressOne",
                                Value = "تهران، خیابان آزادی، کوچه مروارید، پلاک 35",
                                Group= "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZCellPhoneOne",
                                Value = "09356657196",
                                Group= "body"
                            },
	                    #endregion Zamen1
                        #region Zamen2
                            new TemplateModel()
                            {
                                Key = "ZamenTwo",
                                Value = "شرکت فناوری نوین",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZFarzandTwo",
                                Value = "تجاری",
                                Group = "body"
                            },                           
                            new TemplateModel()
                            {
                                Key = "ZStartDateTwo",
                                Value = "1403/01/25",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZIdNumberTwo",
                                Value = "224546366987",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZIssuedByTwo",
                                Value = "کاشان",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZNCTwo",
                                Value = "480857412",
                                Group = "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZPhTwo",
                                Value = "021-95687452",
                                Group= "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZAddressTwo",
                                Value = "تهران، خیابان فردوسی، کوچه شاهنامه، پلاک 1145",
                                Group= "body"
                            },
                            new TemplateModel()
                            {
                                Key = "ZCellPhoneTwo",
                                Value = "09134785632",
                                Group= "body"
                            },
                            #endregion Zamen2
                    #endregion ضامنین
                    #region مورد_معامله
                        new TemplateModel()
                        {
                            Key = "Dastgah" ,
                            Value = "ملک 500 متری" ,
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Company" ,
                            Value = "در خیابان مطهری" ,
                            Group= "body"
                        },
                #endregion مورد_معامله
                    #region قیمت_مورد_معامله
                        new TemplateModel()
                        {
                            Key = "GheymatAddad",
                            Value = "20,000,000,000",
                            Group = "body"
                        },
                        new TemplateModel()
                        {
                            Key = "GheymatHorof",
                            Value = "بیست میلیارد",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "KolGeymatHorof",
                            Value = "25,587,000,000",
                            Group= "body"
                        },
                        #endregion قیمت_مورد_معامله
                #endregion body
                #region footer
                    new TemplateModel()
                    {
                        Key = "BFullName",
                        Value = "حسن عباسی",
                        Group = "footer"
                    },
                    new TemplateModel()
                    {
                        Key = "ZFullNameOne",
                        Value = "مجید رخشانی",
                        Group = "footer"
                    },
                    new TemplateModel()
                    {
                        Key = "ZFullNameTwo",
                        Value = "مریم نجات پور",
                        Group = "footer"
                    },

	            #endregion footer
            };
            return templateModels;
        }
        private List<TemplateModel> CreateCreditFacilityData()
        {
            List<TemplateModel> templateModels = new List<TemplateModel>()
            {
                #region مشخصات_متقاضی
                    new TemplateModel()
                    {
                        Key = "CompanyName",
                        Value = "سازه گستر سایپا",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "SabtNo",
                        Value = "500400",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "SabtDate",
                        Value = "1400/02/15",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "IdNo",
                        Value = "874569",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "EcoCode",
                        Value = "45878454878",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "PostalCode",
                        Value = "741477454654",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "CompAddress",
                        Value = "تهران، جنت آباد شمالی",
                        Group = ""
                    },
                    new TemplateModel()
                    {
                        Key = "CompPhone",
                        Value = "02122541256",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "CompFax",
                        Value = "0216325214",
                        Group = "body"
                    },
                #endregion مشخصات_متقاضی
                #region مشخصات_ضامن
                    new TemplateModel()
                {
                    Key = "ZFullName",
                    Value = "محمد مهدی مجید پور",
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZSabtNo",
                    Value = "200600300",
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZSabtDate",
                    Value = "1395/07/20",
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZShenase",
                    Value = "q1234433",
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZEcoCode",
                    Value = "100100200",
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZPostalCode",
                    Value = "100010001000",
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZAddress",
                    Value = "تهران، میدان صنعت، خیابان نصرتی، کوچه فندق، پلاک 323",
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZPhone",
                    Value = "0218745214",
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZFax",
                    Value = "--",
                    Group = "body"
                },

                #endregion مشخصات_ضامن
                #region تسهیلات_مورد_تقاضا
                    new TemplateModel()
                    {
                        Key = "Tashilat",
                        Value = "2,000,000,000",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Bp",
                        Value = "24",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Pag",
                        Value = "3",
                        Group = "body"
                    },
	            #endregion تسهیلات_مورد_تقاضا
                    new TemplateModel()
                    {
                        Key = "InSherekat",
                        Value = "صنعتی ایران تایر",
                        Group = "body"
                    }
            };
            return templateModels ;
        }
    }
}