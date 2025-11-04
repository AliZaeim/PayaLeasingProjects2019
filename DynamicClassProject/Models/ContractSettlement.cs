using DynamicClassProject.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DynamicClassProject.Models
{
    /// <summary>
    /// کلاس تسویه قرارداد
    /// </summary>
    [SpecialClass]
    [DisplayName("تسویه قرارداد")]
    public class ContractSettlement
    {
        //[Key]
        //public int Id { get; set; }
        /// <summary>
        /// 1
        /// نام و نام خانوادگی
        /// </summary>
        [Display(Name = "نام و نام خانوادگی",Order =1)]
        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        [Required]
        public string FullName { get; set; }
        /// <summary>
        /// 2
        /// کد ملی
        /// </summary>
        [Display(Name = "کد ملی", Order = 2)]
        [Required]
        [StringLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string NC { get; set; }
        /// <summary>
        /// 3
        /// شماره قرارداد
        /// </summary>
        [Display(Name = "شماره قرارداد", Order = 3)]
        [Required]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string ContractNumber { get; set; }
        /// <summary>
        /// 4
        /// تاریخ قرارداد
        /// </summary>
        [Display(Name = "تاریخ قرارداد", Order = 4)]
        [Required]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string ContractDate { get; set; }
        /// <summary>
        /// 5
        /// مبلغ تسهیلات
        /// </summary>
        [Display(Name = "مبلغ تسهیلات", Order = 5)]
        [Required]
        public long? FacilityAmount { get; set; }
        /// <summary>
        /// 6
        /// کل اقساط
        /// </summary>
        [Display(Name = "کل اقساط", Order = 6)]
        [Required]
        public long? TotalInstallments { get; set; }
        /// <summary>
        /// 7
        /// مبلغ سررسید
        /// </summary>
        [Display(Name = "مبلغ سررسید", Order = 7)]
        [Required]
        public long? DueAmount { get; set; }
        /// <summary>
        /// 8
        /// مبلغ پرداختی
        /// </summary>
        [Display(Name = "مبلغ پرداختی", Order = 8)]
        [Required]
        public long? PaymentAmount { get; set; }
        /// <summary>
        /// 9
        /// مانده اقساط
        /// </summary>
        [Display(Name = "مانده اقساط", Order = 9)]
        [Required]
        public long? InstallmentBalance { get; set; }
        /// <summary>
        /// 10
        /// سود دیرکرد
        /// </summary>
        [Display(Name = "سود دیرکرد", Order = 10)]
        [Required]
        public long? DelayInterest { get; set; }

        /// <summary>
        /// 11
        /// سود دیرکرد پرداختی
        /// </summary>
        [Display(Name = "سود دیرکرد پرداختی", Order = 11)]
        [Required]
        public long? PaymentDelayInterest { get; set; }
        /// <summary>
        /// 12
        /// مانده سود
        /// </summary>
        [Display(Name = "مانده سود", Order = 12)]
        [Required]
        [Formula]
        public long? InterestRemain { get; set; }
        /// <summary>
        /// 13
        /// جریمه دیرکرد
        /// </summary>
        [Display(Name = "جریمه دیرکرد", Order = 13)]
        [Required]
        [Formula]
        public long? DelayPenalty { get; set; }
        /// <summary>
        /// 14
        /// جریمه دیرکرد پرداختی
        /// </summary>
        [Display(Name = "جریمه دیرکرد پرداختی", Order = 14)]
        [Required]
        public long? PaymentDelayPenalty { get; set; }
        /// <summary>
        /// 15
        /// مانده جریمه
        /// </summary>
        [Display(Name = "مانده جریمه", Order = 15)]
        [Required]
        public long? PenaltyRemain { get; set; }
        /// <summary>
        /// 16
        /// اقساط آتی
        /// </summary>
        [Display(Name = "اقساط آتی", Order = 16)]
        [Required]
        public long? FutureInstallments { get; set; }
        /// <summary>
        /// 17
        /// پرداختی اقساط آتی
        /// </summary>
        [Display(Name = "پرداختی اقساط آتی", Order = 17)]
        [Required]
        public long? PaymentFutureInstallments { get; set; }
        /// <summary>
        /// 18
        /// مانده اقساط آتی
        /// </summary>
        [Display(Name = "مانده اقساط آتی", Order = 18)]
        [Required]
        public long? FutureInstallmentsRemain { get; set; }
        /// <summary>
        /// 19
        /// نوع اقساط آتی
        /// </summary>
        [Display(Name = "نوع اقساط آتی", Order = 19)]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string FutureInstallmentsType { get; set; }
        /// <summary>
        /// 20
        ///  درصد نوع اقساط آتی
        /// </summary>
        [Display(Name = "درصد نوع اقساط آتی", Order = 20)]
        [Formula]
        public long DynamicPercentofSubFutureInstallments { get; set; }
        /// <summary>
        /// 21
        /// ده درصد نوع اقساط آتی
        /// </summary>
        [Display(Name = "باقیمانده درصد نوع اقساط آتی")]
        [Formula]
        public long RemainDynamicPercentofSubFutureInstallments { get; set; }
        /// <summary>
        /// 22
        /// قابل پرداخت
        /// </summary>
        [Display(Name = "قابل پرداخت")]
        [Formula()]
        public long? PayableValue { get; set; }

    }
}