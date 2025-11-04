using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace DynamicClassProject.Models.Entities
{
    public class ClassData
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="نام فیلد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string FieldName { get; set; }
        [Display(Name = "نام فیلد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string FieldValue { get; set; }
        [Display(Name = "نام فیلد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string FieldType { get; set; } 
        [Display(Name = "فرمول فیلد")]        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string FieldFormula { get; set; } 
        
        public int? ClassInfoId { get; set; }
        #region Relations
        [ForeignKey(nameof(ClassInfoId))]
        public ClassInfo ClassInfo { get; set; }
        #endregion
    }
}