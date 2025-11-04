using DynamicClassProject.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DynamicClassProject.Models
{
    [SpecialClass]
    [DisplayName("کلاس آزمایشی اول")]
    public class TestModelOne
    {
        [Display(Name ="نام")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "فامیلی")]
        [Required]
        public string Family { get; set; }
        [Display(Name = "طول")]
        [Required]
        public int? Length { get; set; }
        [Display(Name = "عرض")]
        [Required]
        public int? Width { get; set; }
        [Display(Name = "مساحت")]
        [Formula]
        public long? Area { get; set; }
        [Display(Name = "محیط")]
        [Formula]
        public long? Perimeter { get; set; }
    }
}