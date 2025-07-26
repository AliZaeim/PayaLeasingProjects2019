using PayaInsProj.Models.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayaInsProj.Models.ViewModels
{
    public class InsuranceReportViewModel
    {
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کارکتر باشد!")]
        [Display(Name = "از تاریخ")]
        [RegularExpression(@"^1\d{3}\/(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])$",
            ErrorMessage = "تاریخ نامعتبر است !")]
        public string StartDate { get; set; }
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کارکتر باشد!")]
        [Display(Name = "تا تاریخ")]
        [RegularExpression(@"^1\d{3}\/(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])$",
            ErrorMessage = "تاریخ نامعتبر است !")]
        public string EndDate { get; set; }
        [Display(Name = "تعداد روزهای هشدار")]

        public int? AlarmDays { get; set; } = 300;
        public int AlarmDaysCount { get; set; } = 0;
        public List<InsInfoDto> InsInfoDtos { get; set; } = new List<InsInfoDto>();
        public List<(string propertyName, string DisplayTitle, dynamic AttributeValue, int Order)> InsInfoDtosInformations { get; set; } = new List<(string propertyName, string DisplayTitle, dynamic AttributeValue, int Order)>();
    }
}