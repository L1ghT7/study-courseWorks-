namespace ForeignTradeContractsWorkstation.App.Database.Contexts.Directory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Counterparties : IBaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Counterparties()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [Column("conterparties_key")]
        public long Id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(200)]
        public string full_name { get; set; }

        [StringLength(50)]
        public string checking_account { get; set; }

        [StringLength(50)]
        public string UNP { get; set; }

        [StringLength(50)]
        public string type_of_counterparty { get; set; }

        [StringLength(100)]
        public string legal_address { get; set; }

        [StringLength(100)]
        public string mailing_address { get; set; }

        [StringLength(150)]
        public string telephones { get; set; }

        [StringLength(200)]
        public string main_contract { get; set; }

        [StringLength(20)]
        public string CBU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
