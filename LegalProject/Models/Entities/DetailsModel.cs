using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegalProject.Models.Entities
{
    public class DetailsModel
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// نوع اقدام
        /// </summary>
        [Display(Name = "نوع اقدام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ActionType { get; set; }
        /// <summary>
        /// تاریخ اقدام
        /// </summary>
        [Display(Name = "تاریخ اقدام")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ActionDate { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [Display(Name = "توضیحات")]
        [StringLength(200,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string Comment { get; set; }
        /// <summary>
        /// شماره پرونده
        /// </summary>
        [Display(Name = "شماره پرونده")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CaseNumber { get; set; }
        /// <summary>
        /// کد رهگیری
        /// </summary>
        [Display(Name = "کد رهگیری")]
        [StringLength(50,ErrorMessage ="{0} حداکثر {1} کاراکتر می تواند باشد!")]
        public string TrackingCode { get; set; }
        public int? MainModelId { get; set; }

        #region Relations
        [ForeignKey(nameof(MainModelId))]
        public MainModel MainModel { get; set; }
        #endregion
    }
}