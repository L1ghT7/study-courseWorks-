﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignTradeContractsWorkstation.App.Database.Contexts
{
    public interface IBaseEntity
    {
         long Id { get; set; }
    }
}
