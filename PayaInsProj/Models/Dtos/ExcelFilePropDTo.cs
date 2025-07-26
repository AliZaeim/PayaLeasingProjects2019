using System.ComponentModel.DataAnnotations;

namespace PayaInsProj.Models.Dtos
{
    public class ExcelFilePropDTo
    {
        [Display(Name = "ردیف")]
        public string Radif { get; set; }
        [Display(Name = "تاریخ صدور")]
        public string IssuedDate { get; set; }
        [Display(Name = "بیمه گذار")]
        public string Insurer { get; set; }
        [Display(Name = "نوع خودرو")]
        public string CarType { get; set; }
        [Display(Name = "شماره بیمه نامه")]
        public string IssueNumber { get; set; }
        [Display(Name = "تاریخ شروع")]
        public string IssueBeginDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        public string IssueEndDate { get; set; }
        [Display(Name = "شماره پلاک")]
        public string Plaque { get; set; }
        [Display(Name = "شماره موتور")]
        public string Engine { get; set; }
        [Display(Name = "شماره شاسی")]
        public string Chasis { get; set; }
        [Display(Name = "خالص حق بیمه + مالیات و عوارض ارزش افزوده")]
        public string NutInsValuewithTax { get; set; }
        [Display(Name = "کد ملی بیمه گذار")]
        public string InsurerNC { get; set; }
        [Display(Name = "سال بیمه نامه")]
        public string InsYear { get; set; }
    }
}