using System.ComponentModel.DataAnnotations;

namespace DynamicClassProj.Models.Entities
{
    public class FormulaDefinition
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100,ErrorMessage ="{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Display(Name ="نام کلاس")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Display(Name ="نام فیلد")]
        public string FieldName { get; set; } // مثل "TotalSalary"
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Display(Name ="فرمول")]
        public string FormulaText { get; set; } // مثل "BaseSalary + Bonus"
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        [Display(Name ="نام فرمول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FormulaName { get; set; }

        [Display(Name ="شــــرح")]
        public string Description { get; set; }
        [Display(Name ="فعال/غیرفعال")]
        public bool IsActive { get; set; } = true;
    }
}