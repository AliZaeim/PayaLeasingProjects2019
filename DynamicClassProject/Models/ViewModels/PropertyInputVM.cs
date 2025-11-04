using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DynamicClassProject.Models.ViewModels
{
    /// <summary>
    /// ویژگی های فیلدهای کلاس
    /// </summary>
    public class PropertyInputVM
    {
        /// <summary>
        /// نام اصلی
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// نام نمایشی
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// نوع
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// نوبت نمایش
        /// </summary>
        public int Order { get; set; }
        public bool IsFormula { get; set; }
        public bool IsNumeric { get; set; }
        public bool IsRequired { get; set; }
        /// <summary>
        /// فرمول
        /// </summary>
        public string PropFormula { get; set; } = string.Empty;
        /// <summary>
        /// مقدار
        /// </summary>
        public string Value { get; set; } = string.Empty;  // string because we bind from form
        public IEnumerable<ValidationAttribute> Validations { get; set; }
    }
}