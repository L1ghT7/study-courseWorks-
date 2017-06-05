using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignTradeContractsWorkstation.App.Database.Contexts.Directory.Entities
{
    public class OrdersGoods : IBaseEntity
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Order")]
        public long OrderId { get; set; }
        [ForeignKey("Goods")]
        public long GoodsId { get; set; }
        public decimal TotalSum { get; set; }

        public decimal TotalAmount { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Goods Goods { get; set; }
    }
}
