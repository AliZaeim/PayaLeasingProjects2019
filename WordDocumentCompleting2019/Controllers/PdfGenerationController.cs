using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordDocumentCompleting2019.Helpers;

namespace WordDocumentCompleting2019.Controllers
{
    public class PdfGenerationController : Controller
    {
        // GET: PdfGeneration
        /// <summary>
        /// بیع نامه
        /// </summary>
        /// <returns></returns>
        public ActionResult SalesLetterForm()
        {
            Dictionary<string, dynamic> keyValues =CreateSalesFormData();
            
            string virtualPath = "~/App_Data/GenPdfs";
            string fileName = "SalesLetterForm.pdf"; // Sanitize user-provided filenames!
            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);

            string PdfVirtualpath = "~/App_Data/Pdfs";
            string PdffileName = $"SalesLetterFormComp-{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfserverPath = Server.MapPath(PdfVirtualpath);
            string PdfPath = Path.Combine(PdfserverPath, PdffileName);
            //var words = PdfTempService.ExtractEnglishWords(fullPath);
            PdfTempService.FillPdfForm(fullPath, PdfPath, keyValues, true);
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(PdfPath);
            string contentType = MimeMapping.GetMimeMapping(PdfPath);
            return File(Pdffiledata, contentType, PdffileName);
        }
        /// <summary>
        /// پیوست شماره یک قرارداد تسهیلات فروش اقساطی
        /// </summary>
        /// <returns></returns>
        public ActionResult AttachmentNo1()
        {
            Dictionary<string, dynamic> keyValues = CreateAttNo1Data();

            string virtualPath = "~/App_Data/GenPdfs";
            string fileName = "AttachmentNo1.pdf"; // Sanitize user-provided filenames!
            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);

            string PdfVirtualpath = "~/App_Data/Pdfs";
            string PdffileName = $"AttachmentNo1Comp-{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfserverPath = Server.MapPath(PdfVirtualpath);
            string PdfPath = Path.Combine(PdfserverPath, PdffileName);
            
            PdfTempService.FillPdfForm(fullPath, PdfPath, keyValues, true);
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(PdfPath);
            string contentType = MimeMapping.GetMimeMapping(PdfPath);
            return File(Pdffiledata, contentType, PdffileName);
        }
        /// <summary>
        /// پرسشنامه درخواست تسهیلات اعتباری
        /// </summary>
        /// <returns></returns>
        public ActionResult CreditFacility()
        {
            Dictionary<string, dynamic> keyValues = CreateCreditFacilityData();

            string virtualPath = "~/App_Data/GenPdfs";
            string fileName = "CreditFacility.pdf"; // Sanitize user-provided filenames!
            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);

            string PdfVirtualpath = "~/App_Data/Pdfs";
            string PdffileName = $"CreditFacilityComp-{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfserverPath = Server.MapPath(PdfVirtualpath);
            string PdfPath = Path.Combine(PdfserverPath, PdffileName);
            
            PdfTempService.FillPdfForm(fullPath, PdfPath, keyValues, true);
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(PdfPath);
            string contentType = MimeMapping.GetMimeMapping(PdfPath);
            return File(Pdffiledata, contentType, PdffileName);
        }
        /// <summary>
        /// قرارداد تسهیلات فروش اقساطی خودرو
        /// </summary>
        /// <returns></returns>
        public ActionResult CarInstallment()
        {
            Dictionary<string, dynamic> keyValues = CreateCarInstallmentData();

            string virtualPath = "~/App_Data/GenPdfs";
            string fileName = "CarInstallment.pdf"; // Sanitize user-provided filenames!
            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);

            string PdfVirtualpath = "~/App_Data/Pdfs";
            string PdffileName = $"CarInstallmentComp-{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfserverPath = Server.MapPath(PdfVirtualpath);
            string PdfPath = Path.Combine(PdfserverPath, PdffileName);

            PdfTempService.FillPdfForm(fullPath, PdfPath, keyValues, true);
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(PdfPath);
            string contentType = MimeMapping.GetMimeMapping(PdfPath);
            return File(Pdffiledata, contentType, PdffileName);
        }
        /// <summary>
        /// فرم گواهی تحویل و قبول خودرو
        /// </summary>
        /// <returns></returns>
        public ActionResult VehicleDeliveryForm()
        {
            Dictionary<string, dynamic> keyValues = CreateVehicleDeliveryData();

            string virtualPath = "~/App_Data/GenPdfs";
            string fileName = "VahicleDeliveryForm.pdf"; // Sanitize user-provided filenames!
            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);

            string PdfVirtualpath = "~/App_Data/Pdfs";
            string PdffileName = $"VehicleDeliveryFormComp-{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfserverPath = Server.MapPath(PdfVirtualpath);
            string PdfPath = Path.Combine(PdfserverPath, PdffileName);

            PdfTempService.FillPdfForm(fullPath, PdfPath, keyValues, true);
            
            byte[] Pdffiledata = System.IO.File.ReadAllBytes(PdfPath);
            string contentType = MimeMapping.GetMimeMapping(PdfPath);
            return File(Pdffiledata, contentType, PdffileName);
        }
        /// <summary>
        /// فرم گواهی تحویل و قبول خودرو
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsDeliveryForm()
        {
            Dictionary<string, dynamic> keyValues = CreateGoodsDeliveryData();

            string virtualPath = "~/App_Data/GenPdfs";
            string fileName = "GoodsDeliveryForm.pdf"; // Sanitize user-provided filenames!
            string serverPath = Server.MapPath(virtualPath);
            string fullPath = Path.Combine(serverPath, fileName);

            string PdfVirtualpath = "~/App_Data/Pdfs";
            string PdffileName = $"GoodsDeliveryFormComp-{DateTime.Now:yyyyMMddTHHmmss}.pdf";
            string PdfserverPath = Server.MapPath(PdfVirtualpath);
            string PdfPath = Path.Combine(PdfserverPath, PdffileName);

            PdfTempService.FillPdfForm(fullPath, PdfPath, keyValues, true);

            byte[] Pdffiledata = System.IO.File.ReadAllBytes(PdfPath);
            string contentType = MimeMapping.GetMimeMapping(PdfPath);
            return File(Pdffiledata, contentType, PdffileName);
        }
        #region TestData
        private Dictionary<string, dynamic> CreateSalesFormData()
        {
            Dictionary<string, dynamic> keyValues = new Dictionary<string, dynamic>();
            keyValues.Add("ShoPaz1", "2025207");
            keyValues.Add("Tar1", "1404/06/20");

            keyValues.Add("Foroshande", "شرکت پایا");
            keyValues.Add("FFarzand", "خصوصی");
            keyValues.Add("Tavalod", "1400/01/01");
            keyValues.Add("FShoShe", "100200");
            keyValues.Add("FSadereh", "تهران");
            keyValues.Add("FShoMel", "400555");
            keyValues.Add("FAddress", "تهران، سعادت آباد");
            keyValues.Add("FShoEgh", "14001010");
            keyValues.Add("FPhone", "0218825632");
            keyValues.Add("FCellphone", "09115424152");
            keyValues.Add("FPostalcode", "1400/01/01");

            keyValues.Add("Kharidar", "علی اصل زعیم");
            keyValues.Add("KFarzand", "کرمعلی");
            keyValues.Add("KTavalod", "1358/09/02");
            keyValues.Add("KShoShe", "543");
            keyValues.Add("KSadereh", "کرج");
            keyValues.Add("KShoMel", "4899068549");
            keyValues.Add("KAddress", "کرج، هشتگرد");
            keyValues.Add("KShoEgh", "404505");
            keyValues.Add("KPhone", "02644632837");
            keyValues.Add("KCellphone", "09126617096");
            keyValues.Add("KPostalcode", "5555542421");

            keyValues.Add("Dastgah", "پژو پارس");
            keyValues.Add("System", "XU7");
            keyValues.Add("Tip", "سال");
            keyValues.Add("Model", "1395");
            keyValues.Add("Color", "مشکی متالیک");
            keyValues.Add("EngNum", "En100200452");
            keyValues.Add("ChaNum", "741258742Ch");
            keyValues.Add("RahRan", "68 ایران" + "376" + "س" + "74");
            keyValues.Add("KeDaraye", "برگ سبز");
            keyValues.Add("ShoFac", "140425132");
            keyValues.Add("Mov", "1402/12/20");
            keyValues.Add("Daf", "شماره 5 تهران");
            keyValues.Add("MorMo", "1,000,000,000");
            keyValues.Add("Mablagh", "400,000,000");
            keyValues.Add("BeMojeb", "چک ملت");
            keyValues.Add("MabBaghi", "600,000,000");


            keyValues.Add("ShoPaz2", "2025207");
            keyValues.Add("Tar2", "1404/06/20");
            keyValues.Add("KasrMab", "10,000,000");
            keyValues.Add("Shaba", "147852369321456987258963");
            keyValues.Add("BeFas", "14");
            keyValues.Add("TanzimTar", "1404/04/15");

            return keyValues;
        }
        private Dictionary<string, dynamic> CreateAttNo1Data()
        {
            Dictionary<string, dynamic> keyValues = new Dictionary<string, dynamic>
            {
                { "ShoPaz", "140410250" },
                { "Tar", "1404/04/30" },
                { "MabNagh", "2,500,000,000" },
                { "MabTas", "4,500,000,000" },
                { "MabAsl", "500,000,000,000" },
                { "Nerkh", "5 درصد" },
                { "TedadGhest", "48" },
                { "Fasele", "3" },
                { "TarShoGhe", "1404/09/01" }
            };
            return keyValues;
        }
        private Dictionary<string, dynamic> CreateCreditFacilityData()
        {
            Dictionary<string, dynamic> keyValues = new Dictionary<string, dynamic>();
            keyValues.Add("MName", "ایران خودرو");
            keyValues.Add("MSabt", "741254");
            keyValues.Add("MTarSabt", "1339/11/01");
            keyValues.Add("MSheMel", "4715754545");
            keyValues.Add("MCodeEgh", "445214");
            keyValues.Add("MPostalCode", "54542178654");
            keyValues.Add("MAddress", "تهران، کیلومتر 10 جاده مخصوص");
            keyValues.Add("MPhone", "0216523254");
            keyValues.Add("MFax", "02147152362");

            keyValues.Add("ZName", "ایران تایر");
            keyValues.Add("ZSabt", "875154");
            keyValues.Add("ZTarSabt", "1355/09/01");
            keyValues.Add("ZSheMel", "899232482");
            keyValues.Add("ZCodeEgh", "354588748");
            keyValues.Add("ZPostalCode", "665545485");
            keyValues.Add("ZAddress", "تهران، کیلومتر 4 جاده مخصوص");
            keyValues.Add("ZPhone", "0216542135");
            keyValues.Add("ZFax", "021854884545");

            keyValues.Add("Tas", "500,000,000,000");
            keyValues.Add("Zaman", "60");
            keyValues.Add("Favasel", "3");
            keyValues.Add("Sherekat", "ایران خودرو");
            return keyValues;
        } 

        private Dictionary<string, dynamic> CreateCarInstallmentData()
        {
            Dictionary<string,dynamic> keyValues = new Dictionary<string,dynamic>();
            #region Page1
            keyValues.Add("ShGhar1", "14044121");
            keyValues.Add("Tar1", "1404/04/31");
            keyValues.Add("KhName", "پایا لیزینگ");
            keyValues.Add("KhFarzand", "اعتبار");
            keyValues.Add("KhTavalod", "1404/04/31");
            keyValues.Add("KhShoShe", "5421");
            keyValues.Add("KhSadereh", "تهران");
            keyValues.Add("KhId", "1254123");
            keyValues.Add("KhAddress", "تهران، سعادت آباد");
            keyValues.Add("KhPhone", "0216657485");
            keyValues.Add("KhCellphone", "0912125425");

            keyValues.Add("Z1Name", "علی رضایی");
            keyValues.Add("Z1Farzand", "محمد رضا");
            keyValues.Add("Z1Tavalod", "1360/02/15");
            keyValues.Add("Z1Sabt", "4125");
            keyValues.Add("Z1Sadereh", "کـــــرج");
            keyValues.Add("Z1Id", "747215");
            keyValues.Add("Z1Phone", "0215478541");
            keyValues.Add("Z1Cellphone", "09125412563");
            keyValues.Add("Z1Address", "تهران، خیابان ولی عصر، خ مطهری، کوچه میرعماد، پلاک 225");

            keyValues.Add("Z2Name", "مجید محمدی");
            keyValues.Add("Z2Farzand", "کرمعلی");
            keyValues.Add("Z2Tavalod", "1366/11/21");
            keyValues.Add("Z2Sabt", "14526");
            keyValues.Add("Z2Sadereh", "تهران");
            keyValues.Add("Z2Id", "4152");
            keyValues.Add("Z2Phone", "0215478541");
            keyValues.Add("Z2Cellphone", "09125412563");
            keyValues.Add("Z2Address", "تهران، خیابان ولی عصر، خ مطهری، کوچه میرعماد، پلاک 225");

            //مورد معامله
            keyValues.Add("MMSystem", "ماشین پژوپارس");
            keyValues.Add("MMSherekat", "ایران خودرو");
            //قیمت مورد معامله
            keyValues.Add("GhNaghdiAddad", "20,000,000,000");
            keyValues.Add("GhNaghdiHorof", "بیست میلیارد");

            keyValues.Add("KolForoshAgh", "500,000,000");
            keyValues.Add("KolForoshAghHorof", "پانصد میلیارد");
            keyValues.Add("NaghdiAdad", "250,000,000");/**/
            keyValues.Add("ForoshAghNHorof", "دویست و پنجاه میلیون");

            keyValues.Add("ّForoshAghM", "150,000,000");/**/
            keyValues.Add("ّForoshAghMHorof", "یکصد و پنجاه میلیون");/**/
            keyValues.Add("Teye", "8");
            keyValues.Add("FaseleZ", "3");
           
            keyValues.Add("AvalinGhAdad", "50,000,000");
            keyValues.Add("AvalinGhHorof", "پنجاه میلیون");
            keyValues.Add("AvalinGhSarresid", "1403/09/25");

            keyValues.Add("GhBaediAdad", "25,000,000");
            keyValues.Add("GhBaediHorof", "بیست و پنج میلیون");
            keyValues.Add("GhBaediFz", "سه ماه");

            #endregion Page1
            #region Page2
            keyValues.Add("ShGhar2", "14044121");
            keyValues.Add("Tar2", "1404/04/31");
            keyValues.Add("Takhir", "500,000");

            keyValues.Add("Chk1Bank", "پاسارگاد");
            keyValues.Add("Chk1ShoH", "325541214741");
            keyValues.Add("Chk1Tar", "1404/02/22");
            keyValues.Add("Chk1Mab", "50,000,000");
            keyValues.Add("Chk1Sho", "254212454");
            keyValues.Add("Chk1SName", "علیرضا شیرزاد");

            
            keyValues.Add("Chk2Bank", "صادرات");
            keyValues.Add("Chk2ShoH", "4141121545");
            keyValues.Add("Chk2Tar", "1403/05/30");
            keyValues.Add("Chk2Mab", "50,000,000");
            keyValues.Add("Chk2Sho", "254212454");
            keyValues.Add("Chk2SName", "مجید شیرمحمدی");

           
            keyValues.Add("Chk3Bank", "مـــلت");
            keyValues.Add("Chk3ShoH", "325541214741");
            keyValues.Add("Chk3Tar", "1404/02/22");
            keyValues.Add("Chk3Mab", "50,000,000");
            keyValues.Add("Chk3Sho", "254212454");
            keyValues.Add("Chk3SName", "علیرضا آقائی");

            keyValues.Add("KhTakhirH", "بیست و پنج میلیون");
            keyValues.Add("KhTakhirA", "25,000,000");
            keyValues.Add("HaghBGh1", "5,000,000");
            keyValues.Add("HaghBGh1H", "پنج میلیون");
            keyValues.Add("HaghBSAdad", "45,000,000");
            keyValues.Add("HaghBSHorof", "چهل پنج میلیون");
            #endregion Page2
            #region Page3
            keyValues.Add("ShGhar3", "14044121");
            keyValues.Add("Tar3", "1404/04/31");
            keyValues.Add("Noskhe", "1404/04/31");
            #endregion Page3


            return keyValues;
        }
        private Dictionary<string, dynamic> CreateVehicleDeliveryData()
        {
            Dictionary<string,dynamic> keyValues = new Dictionary<string, dynamic>
            {
                { "ShoGh", "142020" },
                { "Tar", "1400/02/20" },
                { "TarTah", "1400/03/20" },
                { "Injaneb", "محمد رضا هدایتی" },
                { "VSystem", "پژو پارس سال" },
                { "VModel", "1396" },
                { "VColor", "مشکی متالیک" },
                { "VEngNo", "ENG1020321" },
                { "VChassisNo", "Chs1020321" },
                { "Kh0", "*" },
                { "KhK", "*" },
                { "PelakMov", "د" + "85"  + " 452" + "-33" },
                { "PelakEnGhabli", "س" + "74"  + " 376" + "-68" },
                { "PelakDaeem",  "س" + "74"  + " 376" + "-68" },
                { "PelakEnJadid",  "س" + "74"  + " 376" + "-68" },
                { "Tozihat", "این توضیحات است" },

                { "Zapas", "*" },
                { "Jack", "*" },
                { "Garanti", "*" },

                { "Radio", "*" },
                { "Sayer1", "این سایر اول است" },
                { "Sanad", "*" },
                { "Shenasname", "*" },
                { "Sales", "*" },
                { "Talaei", "*" },
                { "Badaneh", "*" },
                { "Sayer2", "این سایر دوم است" },
                { "NGharardad", "*" },
                { "ListAgh", "*" },
                { "NameKh", "*" },
                { "Sayer3", "این سایر سوم است" },

            };
            return keyValues;
        }
        private Dictionary<string,dynamic> CreateGoodsDeliveryData()
        {
            Dictionary<string,dynamic> keyValues = new Dictionary<string, dynamic>
            {
                { "PazNo", "4042154" },
                { "Tar1", "1404/05/02" },
                { "Injaneb", "محمد مهدی طهماسبی" },
                { "Tar2", "1404/05/02" },


                { "ShKala1", "خودروی دنا پلاس" },
                { "TedadKala1", "1" },
                { "MFani1", "مشخصات فنی 1" },
                
                { "ShKala2", "خودروی دنا پلاس" },                
                { "TedadKala2", "2" },
                { "MFani2", "مشخصات فنی 2" },

                { "ShKala3", "خودروی دنا پلاس" },
                { "TedadKala3", "3" },
                { "MFani3", "مشخصات فنی 3" },

                { "ShKala4", "خودروی دنا پلاس" },
                { "TedadKala4", "4" },
                { "MFani4", "مشخصات فنی 4" },

                { "ShKala5", "خودروی دنا پلاس" },
                { "TedadKala5", "5" },
                { "MFani5", "مشخصات فنی 5" },

                { "Molahezat", "اینجا قسمت ملاحظات است" }
            };
            
            return keyValues;
        }
        #endregion TestData
    }
}