using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContractSettlement.Models.Test
{
    public class Formula
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // مثلا "محاسبه ساده"

        [Required]
        public string Expression { get; set; } // مثال: "A + B * 2"
    }
}