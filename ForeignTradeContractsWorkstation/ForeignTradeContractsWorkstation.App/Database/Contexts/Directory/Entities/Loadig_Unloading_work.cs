namespace ForeignTradeContractsWorkstation.App.Database.Contexts.Directory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Loadig_Unloading_work : IBaseEntity
    {
        [Key]
        [Column("work_key")]
        public long Id { get; set; }

        [StringLength(40)]
        public string kind { get; set; }

        [StringLength(50)]
        public string executor { get; set; }

        [StringLength(50)]
        public string way { get; set; }

        [Column(TypeName = "date")]
        public DateTime? arrival_date { get; set; }

        public TimeSpan? arrival_time { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_of_departure { get; set; }

        public TimeSpan? departure_time { get; set; }

        [StringLength(100)]
        public string addres { get; set; }

        [ForeignKey("Orders")]
        public long? orders_key { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
