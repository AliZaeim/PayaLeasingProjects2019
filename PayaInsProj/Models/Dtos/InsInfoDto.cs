using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayaInsProj.Models.Dtos
{
    public class InsInfoDto
    {
        //[Key]
        //[Display(Name = "شناسه")]
        //public long Id { get; set; }
        /// <summary>
        /// کد ملی بیمه گذار
        /// </summary>
        [Display(Name = "کد ملی بیمه گذار")]
        public string Nationalcode { get; set; }
        /// <summary>
        /// شماره شاسی
        /// </summary>
        [Display(Name = "شاسی")]
        public string ChassisNumber { get; set; }
        /// <summary>
        /// شماره موتور
        /// </summary>
        [Display(Name = "موتور")]
        public string EngineNumber { get; set; }
        /// <summary>
        /// شماره پلاک
        /// </summary>
        [Display(Name = "پـــــــــــــــــلاک")]
        public string PlaqueNo { get; set; }
        /// <summary>
        /// شماره بیمه نامه
        /// </summary>
        [Display(Name = "بیمه نامه")]
        public string InsuranceIssueNumber { get; set; }
        /// <summary>
        /// تاریخ صدور بیمه نامه
        /// </summary>
        [Display(Name = "تاریخ صدور")]
        public string InsuranceIssueDate { get; set; }

        [Display(Name = "شروع بیمه")]
        public string InsurerContractBeginDate { get; set; }
        /// <summary>
        /// تاریخ پایان بیمه نامه
        /// </summary>
        [Display(Name = "پایان بیمه")]
        public string InsurerContractEndDate { get; set; }
        /// <summary>
        ///  سال بیمه
        /// </summary>
        [Display(Name = "سال بیمه")]
        public int InsuranceYear { get; set; }
        /// <summary>
        /// تاریخ درج
        /// </summary>
        [Display(Name = "تاریخ درج")]
        public string InsertDate { get; set; }
        /// <summary>
        /// زمان درج
        /// </summary>
        [Display(Name = "زمان درج")]
        public string InsertTime { get; set; }
        [Display(Name = "بیمه گذار")]
        public string InsurerName { get; set; }
        [Display(Name = "باقی مانده")]
        public int ValidDays { get; set; }

    }
}