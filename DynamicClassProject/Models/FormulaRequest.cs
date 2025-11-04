using System.Collections.Generic;

namespace DynamicClassProject.Models
{
    public class FormulaRequest
    {
        public string Formula { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}