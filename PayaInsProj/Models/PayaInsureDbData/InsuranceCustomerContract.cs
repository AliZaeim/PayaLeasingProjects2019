namespace PayaInsProj.Models.PayaInsureDbData
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InsuranceCustomerContract")]
    public class InsuranceCustomerContract
    {
        
        public InsuranceCustomerContract()
        {
            InsuranceDetailCustomerContracts = new HashSet<InsuranceDetailCustomerContract>();
        }

        public long ID { get; set; }

        public long? ContractID { get; set; }

        public long? SupplierID { get; set; }

        [StringLength(10)]
        public string Nationalcode { get; set; }

        [StringLength(150)]
        public string ChassisNumber { get; set; }

        [StringLength(150)]
        public string EngineNumber { get; set; }

        [StringLength(50)]
        public string PlaqueNo { get; set; }

        public decimal? GoodPrice { get; set; }

        public decimal? InsuranceAmount { get; set; }

        public byte? InsuranceReceiveTypeID { get; set; }

        [StringLength(25)]
        public string InsuranceIssueNumber { get; set; }

        [StringLength(10)]
        public string InsuranceIssueDate { get; set; }

        public int? InsurerContractYear { get; set; }

        [StringLength(10)]
        public string InsurerContractDate { get; set; }

        [StringLength(10)]
        public string InsurerContractBeginDate { get; set; }

        [StringLength(10)]
        public string InsurerContractEndDate { get; set; }

        public int? InsuranceYearCount { get; set; }

        [StringLength(10)]
        public string InsertDate { get; set; }

        [StringLength(10)]
        public string InsertTime { get; set; }

        public long? InsertUserID { get; set; }

        public bool? IsActive { get; set; }

        public bool? RequestConfirm { get; set; }

        public int? ResultNotConfirmID { get; set; }

        [StringLength(500)]
        public string RequestDescription { get; set; }

        public long? ConfirmNotConfirmUserID { get; set; }

        public bool? ConfirmIssue { get; set; }

        [StringLength(10)]
        public string ConfirmIssuDate { get; set; }

        [StringLength(10)]
        public string ConfirmIssuTime { get; set; }

        public long? ConfrimIssuUserId { get; set; }

        public decimal? InsurancAmountInput { get; set; }

        public decimal? InsurancAmountInputTax { get; set; }

        public decimal? InsurancAmountSumInput { get; set; }

        public int? CountIssuing { get; set; }

        public int? CountNotIssuing { get; set; }

        public bool? ManualConvert { get; set; }

        public string InsurerNameLast { get; set; }

        [StringLength(10)]
        public string ManualUpdateDate { get; set; }

        [StringLength(10)]
        public string ManualUpdateTime { get; set; }

        
        public virtual ICollection<InsuranceDetailCustomerContract> InsuranceDetailCustomerContracts { get; set; }
    }
}
