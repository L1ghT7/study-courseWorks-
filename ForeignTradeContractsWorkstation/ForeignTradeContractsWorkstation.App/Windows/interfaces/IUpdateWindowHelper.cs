using ForeignTradeContractsWorkstation.App.Database.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignTradeContractsWorkstation.App.Windows.interfaces
{
    public interface IUpdateWindowHelper<T>
        where T: class,IBaseEntity
    {
        long? CurrentEntityId { get; set; }
    }
}
