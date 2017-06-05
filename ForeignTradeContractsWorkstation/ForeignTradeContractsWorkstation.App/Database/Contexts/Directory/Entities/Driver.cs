namespace ForeignTradeContractsWorkstation.App.Database.Contexts.Directory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [DisplayName("водители")]
    [Table("Driver")]
    public partial class Driver : IBaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Driver()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [Column("driver_key")]
        public long Id { get; set; }

        [StringLength(150)]
        public string full_name { get; set; }

        [StringLength(50)]
        public string telephone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
