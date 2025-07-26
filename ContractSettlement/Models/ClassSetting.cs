namespace ContractSettlement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClassSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassFaTitle { get; set; }

        [Required]
        [StringLength(50)]
        public string PropName { get; set; }

        [Required]
        [StringLength(50)]
        public string PropFaTitle { get; set; }

        [Required]
        [StringLength(300)]
        public string PropFormula { get; set; }
    }
}
