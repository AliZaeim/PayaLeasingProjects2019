using System.ComponentModel.DataAnnotations;

namespace ContractSettlement.Models.ViewModels
{
    public class FormulaViewModel
    {
        // Existing properties
        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "روز")]
        public int Days { get; set; }

        [Display(Name = "مالیات")]
        public float Tax { get; set; }

        // Formula input field
        [Display(Name = "فرمول حقوق")]
        [Required(ErrorMessage = "لطفا فرمول را وارد کنید")]
        public string SalaryFormula { get; set; }

        // Calculated result
        [Display(Name = "حقوق محاسبه شده")]
        public long CalculatedSalary { get; set; }
    }
}