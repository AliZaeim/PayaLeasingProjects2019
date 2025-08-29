using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace DynamicClassProj.Models.Test
{
    [TableName("Formulas")]
    public class Formula
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کلاس")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name ="نام فیلد")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string FieldName { get; set; } // مثلا "C" یا "D"
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "فرمول محاسباتی")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string Expression { get; set; } // مثلا "A + B" یا "A * B + C"
               
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        [Display(Name ="توضیحات")]
        public string Description { get; set; }
    }
}