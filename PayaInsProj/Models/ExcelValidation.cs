using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayaInsProj.Models
{
    public class ExcelValidation
    {
        public bool Valid { get; set; }
        public List<string> ValidationMessages { get; set; } = new List<string>();
    }
}