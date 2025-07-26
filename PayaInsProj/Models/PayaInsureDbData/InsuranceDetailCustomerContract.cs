namespace PayaInsProj.Models.PayaInsureDbData
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InsuranceDetailCustomerContract")]
    public  class InsuranceDetailCustomerContract
    {
        public long ID { get; set; }

        public long? InsuranceCustomerContractID { get; set; }

        [StringLength(25)]
        public string InsuranceIssueNumber { get; set; }

        [StringLength(10)]
        public string InsuranceIssueDate { get; set; }

        public int? InsuranceYear { get; set; }

        [StringLength(10)]
        public string InsuranceBeginDate { get; set; }

        [StringLength(10)]
        public string InsuranceEndDate { get; set; }

        public decimal? InsuranceAmount { get; set; }

        public int? InsuranceNumber { get; set; }

        public int? InsurancePayKind { get; set; }

        public bool? IsIssuing { get; set; }

        [StringLength(10)]
        public string InsertDate { get; set; }

        [StringLength(10)]
        public string InsertTime { get; set; }

        [StringLength(250)]
        public string InsuranceDocument1 { get; set; }

        [StringLength(20)]
        public string InsuranceDocument1Type { get; set; }

        [StringLength(250)]
        public string InsuranceDocument2 { get; set; }

        [StringLength(220)]
        public string InsuranceDocument2Type { get; set; }

        [StringLength(250)]
        public string InsuranceDocument3 { get; set; }

        [StringLength(20)]
        public string InsuranceDocument3Type { get; set; }

        [StringLength(10)]
        public string ConfirmIssuDate { get; set; }

        [StringLength(10)]
        public string ConfirmIssuTime { get; set; }

        public long? ConfrimIssuUserId { get; set; }

        public decimal InsurancAmountInput { get; set; }

        public decimal InsurancAmountInputTax { get; set; }

        public decimal InsurancAmountSumInput { get; set; }

        public string InsurerName { get; set; }

        [StringLength(10)]
        public string ManualUpdateDate { get; set; }

        [StringLength(10)]
        public string ManualUpdateTime { get; set; }

        public virtual InsuranceCustomerContract InsuranceCustomerContract { get; set; }
    }
}
