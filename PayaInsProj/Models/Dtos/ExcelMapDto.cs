using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayaInsProj.Models.Dtos
{
    public class ExcelMapDto
    {
        [Display(Name = "ردیف", Order = 0)]
        public string Rank { get; set; }
        /// <summary>
        /// تاریخ صدور بیمه نامه
        /// </summary>
        public string InsuranceIssueDate { get; set; }
        /// <summary>
        /// بیمه گذار بیمه نامه
        /// </summary>
        public string InsuranceInsurer { get; set; }
        /// <summary>
        /// نوع خودرو
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// شماره کامل بیمه نامه
        /// </summary>
        public string InsurancePolicyNumber { get; set; }
        /// <summary>
        /// تاریخ شروع بیمه نامه
        /// </summary>
        public string InsuranceStartDate { get; set; }
        /// <summary>
        /// تاریخ پایان بیمه نامه
        /// </summary>
        public string InsuranceEndDate { get; set; }
        /// <summary>
        /// شماره پلاک
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// شماره موتور
        /// </summary>
        public string EngineNumber { get; set; }
        /// <summary>
        /// شماره شاسی
        /// </summary>
        public string ChassisNumber { get; set; }
        /// <summary>
        /// خالص حق بیمه + مالیات و عوارض ارزش افزوده
        /// </summary>
        public string NetPremiumVATDuties { get; set; }
        /// <summary>
        /// کد ملی بیمه گذار
        /// </summary>
        public string InsuredNC { get; set; }
        /// <summary>
        /// بیمه نامه سال چندم
        /// </summary>
        public string InsuranceAfterYear { get; set; }
    }
}