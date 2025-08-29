namespace ContractSettlement.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ClassInfo")]
    public partial class ClassInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassInfo()
        {
            PropInfoes = new HashSet<PropInfo>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassFaTitle { get; set; }

        public virtual ClassInfo ClassInfo1 { get; set; }

        public virtual ClassInfo ClassInfo2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropInfo> PropInfoes { get; set; }
    }
}
