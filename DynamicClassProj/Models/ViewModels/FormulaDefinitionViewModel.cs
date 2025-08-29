using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DynamicClassProj.Models.ViewModels
{
    public class FormulaDefinitionViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Display(Name = "نام کلاس")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Display(Name = "نام فیلد")]
        public string FieldName { get; set; } // مثل "TotalSalary"
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Display(Name = "فرمول")]
        public string FormulaText { get; set; } // مثل "BaseSalary + Bonus"
        [Display(Name = "نام فرمول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FormulaName { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        [Display(Name = "شــــرح")]
        public string Description { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; } = true;

        public List<string> ClassNames { get; set; } = new List<string>();
        public List<string> NumericFileds { get; set; } = new List<string>();
        public List<string> FormulaFields { get; set; } = new List<string>();

    }
}