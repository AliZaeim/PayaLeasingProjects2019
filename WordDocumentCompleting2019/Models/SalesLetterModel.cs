using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace WordDocumentCompleting2019.Models
{
    public class SalesLetterModel
    {
        /// <summary>
        /// مشخصات فروشنده
        /// </summary>
        #region SellerInfo
        [Display(Name = "فروشنده",Order =1)]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SellerName { get; set; }
        /// <summary>
        /// فرزند / نوع شرکت
        /// </summary>
        [Display(Name = "فرزند / نوع شرکت",Order =2)]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SRelated { get; set; }
        /// <summary>
        /// تاریخ تولد / تأسیس
        /// </summary>
        [Display(Name = "تاریخ تولد/تأسیس",Order =3)]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SStartDate { get; set; }
        /// <summary>
        /// شماره شناسنامه/ثبت
        /// </summary>
        [Display(Name = "شماره شناسنامه/ثبت")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SellerId { get; set; }
        /// <summary>
        /// صادره از/ محل ثبت
        /// </summary>
        [Display(Name = "صادره از/ محل ثبت")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SSadereh { get; set; }
        /// <summary>
        /// شماره ملی/ شناسنامه ملی
        /// </summary>
        [Display(Name = "شماره ملی/ شناسنامه ملی")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SNC { get; set; }
        /// <summary>
        /// آدرس فرستنده
        /// </summary>
        [Display(Name = "آدرس فروشنده")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SAddress { get; set; }
        /// <summary>
        /// شماره اقتصادی
        /// </summary>
        [Display(Name = "شماره اقتصادی")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SEcoNo { get; set; }
        /// <summary>
        /// شماره تلفن فروشنده
        /// </summary>
        [Display(Name = "شماره تلفن فروشنده")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SPhone { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        [Display(Name = "تلفن همراه")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SCellphone { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        [Display(Name = "کد پستی")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string SPostalCode { get; set; }
        #endregion
        /// <summary>
        /// مشخصات خریدار
        /// </summary>
        #region BuyerInfo
        [Display(Name = "خریدار")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BuyerName { get; set; }
        /// <summary>
        /// فرزند / نوع شرکت
        /// </summary>
        [Display(Name = "فرزند / نوع شرکت")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BRelated { get; set; }
        /// <summary>
        /// تاریخ تولد / تأسیس
        /// </summary>
        [Display(Name = "تاریخ تولد/تأسیس")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BStartDate { get; set; }
        /// <summary>
        /// شماره شناسنامه/ثبت
        /// </summary>
        [Display(Name = "شماره شناسنامه/ثبت")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BuyerId { get; set; }
        /// <summary>
        /// صادره از/ محل ثبت
        /// </summary>
        [Display(Name = "صادره از/ محل ثبت")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BSadereh { get; set; }
        /// <summary>
        /// شماره ملی/ شناسنامه ملی
        /// </summary>
        [Display(Name = "شماره ملی/ شناسنامه ملی")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BNC { get; set; }
        /// <summary>
        /// آدرس فرستنده
        /// </summary>
        [Display(Name = "آدرس خریدار")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BAddress { get; set; }
        /// <summary>
        /// شماره اقتصادی
        /// </summary>
        [Display(Name = "شماره اقتصادی")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BEcoNo { get; set; }
        /// <summary>
        /// شماره تلفن فروشنده
        /// </summary>
        [Display(Name = "شماره تلفن فروشنده")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BPhone { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        [Display(Name = "تلفن همراه")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BCellphone { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        [Display(Name = "کد پستی")]
        //[Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string BPostalCode { get; set; }
        #endregion
        [Display(Name = "دستگاه")]
        public string Dastgah { get; set; }
        [Display(Name = "سیستم")]
        public string System { get; set; }
        [Display(Name = "تیپ")]
        public string Tip { get; set; }
        [Display(Name = "مدل")]
        public string MModel { get; set; }
        [Display(Name = "رنگ")]
        public string Color { get; set; }
        [Display(Name = "شماره موتور")]
        public string EngineNomber { get; set; }
        [Display(Name = "شماره شاسی")]
        public string ChasiseNumber { get; set; }
        [Display(Name = "شماره راهنمایی و رانندگی")]
        public string RahnamiyRanandegoNO { get; set; }
        [Display(Name = "که دارای")]
        public string KeDaraye { get; set; }
        [Display(Name = "شماره سند")]
        public string OrderNomber { get; set; }
        [Display(Name = "تاریخ سند")]
        public string OrderDate { get; set; }
        [Display(Name = "شرکت/دفترخانه")]
        public string Company { get; set; }
        [Display(Name = "بهای مورد معامله")]
        public string BahayeMoredMoeameleh { get; set; }
        [Display(Name = "مبلغ نقدی")]
        public string MablaghMoeameleh { get; set; }
        [Display(Name = "به موجب")]
        public string BeMojebe { get; set; }
        [Display(Name = "مبلغ باقی مانده")]
        public string MablaghbaghiMandeh { get; set; }
        [Display(Name = "شماره شبا")]
        public string ShabaNO { get; set; }
        [Display(Name = "فاصله روز")]
        public string FaseleyeRooz { get; set; }
        [Display(Name = "تاریخ بیع نامه")]
        public string BeynameDate { get; set; }
        public string FooterBuyer { get; set; }


    }
}