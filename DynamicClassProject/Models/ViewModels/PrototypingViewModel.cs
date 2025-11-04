using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DynamicClassProject.Models.ViewModels
{
    public class PrototypingViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Display(Name = "نام کلاس")]
        public string ClassName { get; set; }
        public List<dynamic> Fields { get; set; } = new List<dynamic>();
        public List<string> ClassNames { get; set; } = new List<string>();
        public List<string> NumericFileds { get; set; } = new List<string>();
        public List<string> FormulaFields { get; set; } = new List<string>();
    }
}