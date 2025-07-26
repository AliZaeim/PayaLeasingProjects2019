using System.ComponentModel.DataAnnotations;

namespace ContractSettlement.Models
{
    /// <summary>
    /// تسویه قرارداد
    /// </summary>
    public class ContractSettlementModel
    {
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        /// <summary>
        /// کد ملی
        /// </summary>
        [Display(Name = "کد ملی")]
        public string NC { get; set; }
        /// <summary>
        /// شماره قرارداد
        /// </summary>
        [Display(Name = "شماره قرارداد")]
        public string ContractNumber { get; set; }
        /// <summary>
        /// تاریخ قرارداد
        /// </summary>
        [Display(Name = "تاریخ قرارداد")]
        public string ContractDate { get; set; }
        /// <summary>
        /// مبلغ تسهیلات
        /// </summary>
        [Display(Name = "مبلغ تسهیلات")]
        public long? FacilityAmount { get; set; }
        /// <summary>
        /// کل اقساط
        /// </summary>
        [Display(Name = "کل اقساط")]
        public long? TotalInstallments { get; set; }
        /// <summary>
        /// مبلغ سررسید
        /// </summary>
        [Display(Name = "مبلغ سررسید")]
        public long? DueAmount { get; set; }
        /// <summary>
        /// مبلغ پرداختی
        /// </summary>
        [Display(Name = "مبلغ پرداختی")]
        public long? PaymentAmount { get; set; }
        /// <summary>
        /// مانده اقساط
        /// </summary>
        [Display(Name = "مانده اقساط")]
        public long? InstallmentBalance { get; set; }
        /// <summary>
        /// سود دیرکرد
        /// </summary>
        [Display(Name = "سود دیرکرد")]
        public long? DelayInterest { get; set; }
        /// <summary>
        /// سود دیرکرد پرداختی
        /// </summary>
        [Display(Name = "سود دیرکرد پرداختی")]
        public long? PaymentDelayInterest { get; set; }
        /// <summary>
        /// مانده سود
        /// </summary>
        [Display(Name = "مانده سود")]
        public long? InterestRemain { get; set; }
        /// <summary>
        /// جریمه دیرکرد
        /// </summary>
        [Display(Name = "جریمه دیرکرد")]
        public long? DelayPenalty { get; set; }
        /// <summary>
        /// جریمه دیرکرد پرداختی
        /// </summary>
        [Display(Name = "جریمه دیرکرد پرداختی")]
        public long? PaymentDelayPenalty { get; set; }
        /// <summary>
        /// مانده جریمه
        /// </summary>
        [Display(Name = "مانده جریمه")]
        public long? PenaltyRemain { get; set; }
        /// <summary>
        /// اقساط آتی
        /// </summary>
        [Display(Name = "اقساط آتی")]
        public long? FutureInstallments { get; set; }
        /// <summary>
        /// پرداختی اقساط آتی
        /// </summary>
        [Display(Name = "پرداختی اقساط آتی")]
        public long? PaymentFutureInstallments { get; set; }
        /// <summary>
        /// مانده اقساط آتی
        /// </summary>
        [Display(Name = "مانده اقساط آتی")]
        public long? FutureInstallmentsRemain { get; set; }
        /// <summary>
        /// نوع اقساط آتی
        /// </summary>
        [Display(Name = "نوع اقساط آتی")]
        public string FutureInstallmentsType { get; set; }
        /// <summary>
        /// نود درصد نوع اقساط آتی
        /// </summary>
        [Display(Name = "نود درصد نوع اقساط آتی")]
        public long NinetyPercentofSubFutureInstallments { get; set; }
        /// <summary>
        /// ده درصد نوع اقساط آتی
        /// </summary>
        [Display(Name = "ده درصد نوع اقساط آتی")]
        public long TenPercentofSubFutureInstallments { get; set; }
        /// <summary>
        /// قابل پرداخت
        /// </summary>
        [Display(Name = "قابل پرداخت")]
        public long? PayableValue { get; set; }
    }
}