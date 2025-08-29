using System;
using System.ComponentModel.DataAnnotations;

namespace ContractSettlement_Ver2.Models.Entites
{
    public class SalaryFormula
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "عنوان فرمول")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "متن فرمول")]
        public string FormulaText { get; set; }

        [Display(Name = "نام کلاس")]
        public string ClassName { get; set; }

        public int? UserId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}