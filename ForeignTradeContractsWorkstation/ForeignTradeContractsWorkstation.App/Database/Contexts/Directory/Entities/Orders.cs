namespace ForeignTradeContractsWorkstation.App.Database.Contexts.Directory
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders : IBaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            Loadig_Unloading_work = new HashSet<Loadig_Unloading_work>();
            Goods= new HashSet<OrdersGoods>();
        }

        [Key]
        [Column("orders_key")]
        public long Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_of_order { get; set; }

        [StringLength(5)]
        public string consignment_series { get; set; }

        [StringLength(25)]
        public string consignment_number { get; set; }

        [Range(0,100)]
        public byte VAT { get; set; }

        public decimal TotalSum { get; set; }

        [ForeignKey("Counterparties")]
        public long? Counterparties_key { get; set; }

        [ForeignKey("Coworker")]
        public long? coworker_key { get; set; }

        [ForeignKey("Driver")]
        public long? driver_key { get; set; }

        [ForeignKey("Cars")]
        public long? car_key { get; set; }

        public virtual Cars Cars { get; set; }

        public virtual Counterparties Counterparties { get; set; }

        public virtual Coworker Coworker { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual ICollection<OrdersGoods> Goods { get; set; }

        public virtual ICollection<Loadig_Unloading_work> Loadig_Unloading_work { get; set; }

        [NotMapped]
        public string ToListNumber { get; set; }
        [NotMapped]
        public string ShipCustomer { get; set; }
        [NotMapped]
        public string LeaveBases { get; set; }
        [NotMapped]
        public string PointLoading { get; set; }
        [NotMapped]
        public string PointUnloading { get; set; }
        [NotMapped]
        public string ProxyNumber { get; set; }
        [NotMapped]
        public string ProxyDate { get; set; }
        [NotMapped]
        public string ProxyGiver { get; set; }
        [NotMapped]
        public string AcceptedGuy { get; set; }

    }
}
