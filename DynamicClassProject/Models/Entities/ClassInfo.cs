using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DynamicClassProject.Models.Entities
{
    public class ClassInfo
    {
        public ClassInfo()
        {
            ClassDatas = new List<ClassData>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        [Display(Name ="نام کلاس")]
        public string ClassName { get; set; }
        #region Relations
        public List<ClassData>  ClassDatas { get; set; }
        #endregion
    }
}