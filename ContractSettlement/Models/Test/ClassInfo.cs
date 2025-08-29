using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractSettlement.Models.Test
{
    public class ClassInfo
    {
        public string ClassName { get; set; }
        public string ClassSummary { get; set; }
        public List<PropertyInfo> Properties { get; set; }
    }
}
