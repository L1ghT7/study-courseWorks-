using ForeignTradeContractsWorkstation.App.Windows.Custom_Attributes;

namespace ForeignTradeContractsWorkstation.App.Database.Contexts.Directory
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TableName("Товары")]
    public partial class Goods: IBaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Goods()
        {
            Orders = new HashSet<OrdersGoods>();
        }

        [Key]
        [Column("goods_key")]
        public long Id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(200)]
        public string full_name { get; set; }

        [StringLength(100)]
        public string nomenclature_type { get; set; }

        [StringLength(100)]
        public string item_type { get; set; }

        [StringLength(100)]
        public string Nomenclature { get; set; }

        public float? VAT { get; set; }

        public float? accounting_price { get; set; }

        public float? purchase_price { get; set; }

        public float? manufacturers_price { get; set; }

        [StringLength(50)]
        public string country { get; set; }

        public float? the_weight { get; set; }

        [StringLength(100)]
        public string purpose_of_acquisition { get; set; }

        [ForeignKey("Storage")]
        public long storage_key { get; set; }

       
        public virtual ICollection<OrdersGoods> Orders { get; set; }

        
        public virtual Storage Storage { get; set; }

        [NotMapped]
        public decimal TotalSum { get; set; }
        [NotMapped]
        public decimal TotalAmount { get; set; }
    }
}
