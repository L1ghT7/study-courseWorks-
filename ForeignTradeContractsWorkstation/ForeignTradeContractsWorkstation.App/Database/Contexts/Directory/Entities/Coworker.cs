namespace ForeignTradeContractsWorkstation.App.Database.Contexts.Directory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coworker")]
    public partial class Coworker : IBaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coworker()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [Column("coworker_key")]
        public long Id { get; set; }

        [StringLength(200)]
        public string full_name { get; set; }

        [StringLength(200)]
        public string sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_of_birth { get; set; }

        [StringLength(150)]
        public string telephones { get; set; }

        [StringLength(100)]
        public string adress { get; set; }

        [StringLength(50)]
        public string document_type { get; set; }

        [StringLength(5)]
        public string passport_series { get; set; }

        [StringLength(20)]
        public string passport_ID { get; set; }

        [StringLength(100)]
        public string who_issued { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_of_issue { get; set; }

        [StringLength(100)]
        public string position { get; set; }

        [Column(TypeName = "date")]
        public DateTime? employment_date { get; set; }

        [StringLength(50)]
        public string salary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
