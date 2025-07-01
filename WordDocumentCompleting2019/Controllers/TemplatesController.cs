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
            List<TemplateModel> MyplaceHolders = CreateCarInstallmentData();


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
            List<TemplateModel> MyplaceHolders = CreateCreditFacilityData();


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
            List<TemplateModel> MyplaceHolders = CreateAttachmentNo1Data();


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
            List<TemplateModel> MyplaceHolders = CreateSalesFormData();
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
        //VehicleDeliveryandAcceptanceCertificateForm
        /// <summary>
        ///  فرم گواهی تحویل و قبول خودرو
        /// </summary>
        /// <returns></returns>
        public ActionResult VehicleDeliveryForm()
        {
            List<TemplateModel> MyplaceHolders = CreateVehicleDeliveryaFormData();
            string templatePath = Server.MapPath("~/App_Data/DocumentTemplates/VehicleDeliveryandAcceptanceCertificateForm.docx");
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".docx");

            // Copy template to temp file
            System.IO.File.Copy(templatePath, outputPath, true);

            // Replace placeholders
            WordTemplateHelper.ReplacePlaceholders(outputPath, MyplaceHolders);

            // Return the generated document
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
           
            return GeneratePdf("VehicleDeliveryForm.pdf", fileBytes);
            //return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "VehicleDeliveryForm.docx");
        }
        private List<TemplateModel> CreateCarInstallmentData()
        {
            List<TemplateModel> templateModels = new List<TemplateModel>()
            {
                #region header
                    new TemplateModel()
                    {
                        Key = "agNumber",
                        Value = "404002".ConvertToPersianNumbers(),
                        Group = "header"
                    },
                    new TemplateModel()
                    {
                        Key = "contDate",
                        Value = "1404/02/25".ConvertToPersianNumbers(),
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
                            Value = "1358/09/02".ConvertToPersianNumbers(),
                            Group = "body"
                        },
                        new TemplateModel()
                        {
                            Key = "BIdNo",
                            Value = "543".ConvertToPersianNumbers(),
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
                            Value = "4899068549".ConvertToPersianNumbers(),
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
                            Value = "02644632837".ConvertToPersianNumbers(),
                            Group = "body"
                        },
                        new TemplateModel()
                        {
                            Key = "BCellphone",
                            Value = "09126617096".ConvertToPersianNumbers(),
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
                            Key = "Aghsati",
                            Value = "50,0000,0000",
                            Group = "body",
                            
                            Bold = true 
                        },
                        new TemplateModel()
                        {
                            Key = "Kolgeymathorof",
                            Value = "بیست میلیارد",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mablaghnaghd",
                            Value = "1,000,000,000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mnaghdhorof",
                            Value = "یک میلیارد",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mabaghi",
                            Value = "850,000,000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mabaghihorof",
                            Value = "هشتصد و پنجاه میلیون",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mabaghi",
                            Value = "450,000,000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mabaghihorof",
                            Value = "چهارصد و پنجاه میلیون",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Teye",
                            Value = "48",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Fzamani",
                            Value = "6",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Avalinghest",
                            Value = "50,000,000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Avalinghesthorof",
                            Value = "پنجاه میلیون",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Sarresiddate",
                            Value = "1406/01/15",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Aghsatebadi",
                            Value = "45,000,000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Aghsatbadihorof",
                            Value = "چهل و پنج میلیون",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Faselezamani",
                            Value = "3 ماه".ConvertToPersianNumbers(),
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mtakhir",
                            Value = "450,000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Haghbimehhorof",
                            Value = "هفتصد و پنجاه میلیون",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Haghbimeh",
                            Value = "750,000,000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Ghestavalbimeh",
                            Value = "450,000,000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Ghestavalbimehhorof",
                            Value = "چهار صد و پنجاه میلیون",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Ghestbimeh",
                            Value = "300,000,000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Ghestbimehhorof",
                            Value = "سیصد میلیون",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Tarikhtanzim",
                            Value = "1402/07/17",
                            Group= "body"
                        },
                #endregion قیمت_مورد_معامله
                    #region ضامنها
                        new TemplateModel()
                        {
                            Key = "Sadernameone",
                            Value = "سینا یعقوبی",
                            Group= "body"
                        },                        
                        new TemplateModel()
                        {
                            Key = "Checkone",
                            Value = "1478522154",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mablaghcheckone",
                            Value = "1478522154",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Tarikhone",
                            Value = "1404/09/15",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Shomarehesabone",
                            Value = "5458787545454",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Bankone",
                            Value = "صادرات",
                            Group= "body"
                        },
                        
                        new TemplateModel()
                        {
                            Key = "Sadernametwo",
                            Value = "علیرضا شیرزاد",
                            Group= "body"

                        },
                        new TemplateModel()
                        {
                            Key = "Checktwo",
                            Value = "741777777",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mablaghchecktwo",
                            Value = "7841245445000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Tarikhtwo",
                            Value = "1404/04/04",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Shomarehesabtwo",
                            Value = "400500600",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Banktwo",
                            Value = "ملی",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Sadernamethree",
                            Value = "خانم حسن لو",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Checkthree",
                            Value = "900600200",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Mablaghcheckthree",
                            Value = "50040000000",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Tarikhthree",
                            Value = "1405/05/25",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Shomarehesabthree",
                            Value = "1472121215245",
                            Group= "body"
                        },
                        new TemplateModel()
                        {
                            Key = "Bankthree",
                            Value = "مـــلت",
                            Group= "body"
                        },
	                #endregion ضامنها
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
                        Key = "NameSherekat",
                        Value = "سازه گستر سایپا",
                        Group = "body",
                        FontName = "B Titr",
                        FontSize = 14

                    },
                    new TemplateModel()
                    {
                        Key = "SabtNo",
                        Value = "500400".ConvertToPersianNumbers(),
                        Group = "body",
                        FontName = "B Koodak",
                        FontSize = 20
                    },
                    new TemplateModel()
                    {
                        Key = "SabtDate",
                        Value = "15/02/1400",
                        Group = "body",
                        
                        FontSize = 28
                    },
                    new TemplateModel()
                    {
                        Key = "IdNo",
                        Value = "874569".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "EcoCode",
                        Value = "45878454878".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "PostalCode",
                        Value = "741477454654".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "CompNeshani",
                        Value = "تهران، جنت آباد شمالی",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "CompPhone",
                        Value = "02122541256".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "CompFax",
                        Value = "0216325214".ConvertToPersianNumbers(),
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
                    Value = "200600300".ConvertToPersianNumbers(),
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZSabtDate",
                    Value = "1395/07/20".ConvertToPersianNumbers(),
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZShenase",
                    Value = "q1234433".ConvertToPersianNumbers(),
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZEcoCode",
                    Value = "100100200".ConvertToPersianNumbers(),
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZPostalCode",
                    Value = "100010001000".ConvertToPersianNumbers(),
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZAddress",
                    Value = "تهران، میدان صنعت، خیابان نصرتی، کوچه فندق، پلاک 323".ConvertToPersianNumbers(),
                    Group = "body"
                },
                new TemplateModel()
                {
                    Key = "ZPhone",
                    Value = "0218745214".ConvertToPersianNumbers(),
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
                        Value = "2,000,000,000".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Bp",
                        Value = "24".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Pag",
                        Value = "3".ConvertToPersianNumbers(),
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
        private List<TemplateModel> CreateAttachmentNo1Data()
        {
            List<TemplateModel> templateModels = new List<TemplateModel>()
            {
                #region header
                    new TemplateModel()
                    {
                        Key = "PazNo",
                        Value = "404500".ConvertToPersianNumbers(),
                        Group = "header"
                    },
                    new TemplateModel()
                    {
                        Key = "TarikhPaz",
                        Value = "1404/04/07".ConvertToPersianNumbers(),
                        Group = "header"
                    },
                #endregion
                #region body
                    new TemplateModel()
                    {
                        Key = "MNKharid",
                        Value = "560,000,000".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Mdaryafti",
                        Value = "420,000,000".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "MAFTashilat",
                        Value = "355,0000,000".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "NGharardad",
                        Value = "25".ConvertToPersianNumbers() ,
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "TGhest",
                        Value = "48".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "TFaseleh",
                        Value = "2".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "TarikhShGh",
                        Value = "1403/01/01".ConvertToPersianNumbers(),
                        Group = "body"
                    },
	            #endregion body
            };
            return templateModels;
        }
        private List<TemplateModel> CreateSalesFormData()
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
        private List<TemplateModel> CreateVehicleDeliveryaFormData()
        {
            List<TemplateModel> templateModels = new List<TemplateModel>()
            {
                #region header
                    new TemplateModel()
                    {
                        Key = "ContNo",
                        Value = "720820".ConvertToPersianNumbers(),
                        Group = "header"
                    },
                    new TemplateModel()
                    {
                        Key = "ContDate",
                        Value = "1404/02/28".ConvertToPersianNumbers(),
                        Group = "header"
                    },
                #endregion
                #region body
                    new TemplateModel()
                    {
                        Key = "Tahvildate",
                        Value = "1404/04/10".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Tahvilgirandeh",
                        Value = "رضا محمد زاده",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Nsystem",
                        Value = "XU7- پژو پارس",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Model",
                        Value = "1395".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Color",
                        Value = "مشکی متالیک".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Enginnumber",
                        Value = "EN400564".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Chassisnumber",
                        Value = "ChN720820".ConvertToPersianNumbers(),
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Khs",
                        Value = "\u2610",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "Khk",
                        Value = "\u2611",
                        Group = "body"
                    },
                    new TemplateModel()
                    {
                        Key = "PelakMov",
                        Value = "76د" + "376" + "ایران 68",
                        Group = "body",
                        FontName = "Calibri",
                        FontSize = 24,
                        Bold = true
                    },
                    new TemplateModel()
                    {
                        Key = "Pelakenbef",
                        Value = "88م" + "800" + "ایران 60",
                        Group = "body",
                        FontName = "Calibri",
                        FontSize = 24
                    },
                    new TemplateModel()
                    {
                        Key = "Pelakda",
                        Value = "68   " +  "س74 -" + "367",
                        Group = "body",
                        FontName = "Calibri",
                        FontSize = 24
                    },
                    new TemplateModel()
                    {
                        Key = "Pelakennew",
                        Value = "54ب" + "376" + "ایران 10",
                        Group = "body",
                        FontName = "Calibri",
                        FontSize=24
                    },
                    new TemplateModel()
                    {
                        Key = "Comment",
                        Value = "گواهی تایید شده است.",
                        Group = "body",
                    },
                    
                    new TemplateModel()
                    {
                        Key = "Zapas",
                        Value = "\u2610",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Jack",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Cartg",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Radio",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Sayer",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Sanad",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Shenase",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Sales",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Cartt",
                        Value = "\u2610",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Bimehbadaneh",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Msayer",
                        Value = "---",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Gharardad",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Aghsat",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Nametahvil",
                        Value = "\u2611",
                        Group = "body",
                    },
                    new TemplateModel()
                    {
                        Key = "Mghsayer",
                        Value = "با مشتری به توافق رسیده شده است.",
                        Group = "body",
                    },
	                
                #endregion body
            };
            return templateModels;
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
    }
}