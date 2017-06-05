using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignTradeContractsWorkstation.App.Windows.Custom_Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute: Attribute
    {
        public string TableName { get; set; }

        public TableNameAttribute(string value)
        {
            TableName = value;
        }

    }
}
