namespace ForeignTradeContractsWorkstation.App.Database.Contexts.Directory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

   
   
   
    public partial class Cars : IBaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cars()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [Column("car_key")]
        public long Id { get; set; }

        [StringLength(150)]
        public string type_of_car { get; set; }

        [StringLength(100)]
        public string trailer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
