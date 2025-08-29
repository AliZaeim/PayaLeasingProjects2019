using DynamicClassProj.Utilities;
using System.ComponentModel.DataAnnotations;

namespace DynamicClassProj.Models.Entities
{
    [SpecialClass]
    public class ContractSettlement
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        [Display(Name = "نام و نام خانوادگی")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FullName { get; set; }
        /// <summary>
        /// کد ملی
        /// </summary>
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string NC { get; set; }
        /// <summary>
        /// شماره قرارداد
        /// </summary>
        [Display(Name = "شماره قرارداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string ContractNumber { get; set; }
        /// <summary>
        /// تاریخ قرارداد
        /// </summary>
        [Display(Name = "تاریخ قرارداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string ContractDate { get; set; }
        /// <summary>
        /// مبلغ تسهیلات
        /// </summary>
        [Display(Name = "مبلغ تسهیلات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? FacilityAmount { get; set; }
        /// <summary>
        /// کل اقساط
        /// </summary>
        [Display(Name = "کل اقساط")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? TotalInstallments { get; set; }
        /// <summary>
        /// مبلغ سررسید
        /// </summary>
        [Display(Name = "مبلغ سررسید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? DueAmount { get; set; }
        /// <summary>
        /// مبلغ پرداختی
        /// </summary>
        [Display(Name = "مبلغ پرداختی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? PaymentAmount { get; set; }
        /// <summary>
        /// مانده اقساط
        /// </summary>
        [Display(Name = "مانده اقساط")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? InstallmentBalance { get; set; }
        /// <summary>
        /// سود دیرکرد
        /// </summary>
        [Display(Name = "سود دیرکرد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? DelayInterest { get; set; }

        /// <summary>
        /// سود دیرکرد پرداختی
        /// </summary>
        [Display(Name = "سود دیرکرد پرداختی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? PaymentDelayInterest { get; set; }
        /// <summary>
        /// مانده سود
        /// </summary>
        [Display(Name = "مانده سود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? InterestRemain { get; set; }
        /// <summary>
        /// جریمه دیرکرد
        /// </summary>
        [Display(Name = "جریمه دیرکرد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? DelayPenalty { get; set; }
        /// <summary>
        /// جریمه دیرکرد پرداختی
        /// </summary>
        [Display(Name = "جریمه دیرکرد پرداختی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? PaymentDelayPenalty { get; set; }
        /// <summary>
        /// مانده جریمه
        /// </summary>
        [Display(Name = "مانده جریمه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? PenaltyRemain { get; set; }
        /// <summary>
        /// اقساط آتی
        /// </summary>
        [Display(Name = "اقساط آتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? FutureInstallments { get; set; }
        /// <summary>
        /// پرداختی اقساط آتی
        /// </summary>
        [Display(Name = "پرداختی اقساط آتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? PaymentFutureInstallments { get; set; }
        /// <summary>
        /// مانده اقساط آتی
        /// </summary>
        [Display(Name = "مانده اقساط آتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? FutureInstallmentsRemain { get; set; }
        /// <summary>
        /// نوع اقساط آتی
        /// </summary>
        [Display(Name = "نوع اقساط آتی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string FutureInstallmentsType { get; set; }
        /// <summary>
        ///  درصد نوع اقساط آتی
        /// </summary>
        [Display(Name = "درصد نوع اقساط آتی")]
        [Formula("DynamicPercentofSubFutureInstallmentsFormula")]
        public long DynamicPercentofSubFutureInstallments { get; set; }
        /// <summary>
        /// ده درصد نوع اقساط آتی
        /// </summary>
        [Display(Name = "باقیمانده درصد نوع اقساط آتی")]
       
        public long RemainDynamicPercentofSubFutureInstallments { get; set; }
        /// <summary>
        /// قابل پرداخت
        /// </summary>
        [Display(Name = "قابل پرداخت")]
        
        public long? PayableValue { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string DynamicPercentofSubFutureInstallmentsFormula { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string RemainDynamicPercentofSubFutureInstallmentsFormula { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PayableValueFormula { get; set; }
    }
}