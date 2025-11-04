using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegalProject.Models.Entities
{
    public class MainModel
    {
        public MainModel()
        {
            DetailsModels = new List<DetailsModel>();
        }
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        [Display(Name = "نام")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string Name { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Display(Name = "نام خانوادگی")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string Family { get; set; }
        /// <summary>
        /// کد ملی
        /// </summary>
        [Display(Name = "کد ملی")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string NationalId { get; set; }
        /// <summary>
        /// شماره قرارداد
        /// </summary>
        [Display(Name = "شماره قرارداد")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string ContractNumber { get; set; }
        /// <summary>
        /// شماره چک
        /// </summary>
        [Display(Name = "شماره چک")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string ChequeNumber { get; set; }
        /// <summary>
        /// تاریخ چک
        /// </summary>
        [Display(Name = "تاریخ چک")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string ChequeDate { get; set; }
        /// <summary>
        /// وضعیت چک
        /// </summary>
        [Display(Name = "وضعیت چک")]        
        public int? ChequeState { get; set; }
        /// <summary>
        /// تاریخ دادخواست
        /// </summary>
        [Display(Name = "تاریخ دادخواست")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string PatitionDate { get; set; }
        /// <summary>
        /// شماره بایگانی اجرا
        /// </summary>
        [Display(Name = "شماره بایگانی اجرا")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string ExecArchiveNumber { get; set; }
        /// <summary>
        /// کد شعبه
        /// </summary>
        [Display(Name = "کد شعبه")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string BranchCode { get; set; }
        /// <summary>
        /// اخذ از شعبه
        /// </summary>
        [Display(Name="اخذ از شعبه")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string ObtainFromBranch { get; set; }
        /// <summary>
        /// ثبت در اجرا
        /// </summary>
        [Display(Name="ثبت در اجرا")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string ReginProg { get; set; }
        /// <summary>
        /// استعلام سه گانه
        /// </summary>
        [Display(Name="استعلام سه گانه")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string TripleInquiry { get; set; }
        /// <summary>
        /// تاریخ توقیف
        /// </summary>
        [Display(Name="تاریخ توقیف")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string SeizureDate { get; set; }
        /// <summary>
        /// نوع توقیف 
        /// </summary>
        [Display(Name="نوع توقیف")]
        
        public int? SeizureType { get; set; }
        /// <summary>
        /// استرداد شکایت
        /// </summary>
        [Display(Name="استرداد شکایت")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string ReturnOfComplaint { get; set; }
        /// <summary>
        /// مختومه
        /// </summary>
        [Display(Name="مختومه")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string Closed { get; set; }
        /// <summary>
        /// تاریخ مختومه
        /// </summary>
        [Display(Name="تاریخ مختومه")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string ClosedDate { get; set; }

        #region Relations
        public List<DetailsModel>  DetailsModels { get; set; }
        #endregion
    }
}