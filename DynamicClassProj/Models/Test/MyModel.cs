using DynamicClassProj.Utilities;
using System.ComponentModel.DataAnnotations;

namespace DynamicClassProj.Models.Test
{
    [SpecialClass]
    public class MyModel
    {
       
        [Display(Name ="حقوق")]
        public double A { get; set; }
        [Display(Name ="کارمزد")]
        public double B { get; set; }

        // فیلد فرمول‌دار
        [Formula("ResultFormula")]
        [Display(Name="فرمول 1")]
        public double Result { get; set; }
        [Formula("ResultFormula2")]
        [Display(Name = "فرمول 2")]
        public double Result2 { get; set; }

        // رشته حاوی فرمول برای Result
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100,ErrorMessage ="{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ResultFormula { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100,ErrorMessage ="{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ResultFormula2 { get; set; }
    }
}