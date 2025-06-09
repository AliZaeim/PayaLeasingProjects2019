using System;
using System.ComponentModel.DataAnnotations;

namespace WordDocumentCompleting2019.Models
{
    public class PersonModel
    {
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Display(Name = "Last Name")]
        public string Family { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Father")]
        
        public string Father { get; set; }

        [Display(Name = "Occupation")]
        public string Job { get; set; }
    }
}