using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignTradeContractsWorkstation.App.windows.interfaces
{
    public  interface IBaseWindowContract
    {
        void Show();
        void ShowDialog();
        void Close();
    }
}
