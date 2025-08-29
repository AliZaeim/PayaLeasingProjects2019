namespace ContractSettlement.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PropInfo")]
    public partial class PropInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="نام ویژگی")]
        public string PropName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "عنوان ویژگی")]
        public string PropFaTitle { get; set; }
        [Display(Name = "فرمول محاسباتی ویژگی")]
        [StringLength(300)]
        public string PropFormula { get; set; }

        public int? ClassInfoId { get; set; }
        [ForeignKey("ClassInfoId")]
        public virtual ClassInfo ClassInfo { get; set; }
    }
}
