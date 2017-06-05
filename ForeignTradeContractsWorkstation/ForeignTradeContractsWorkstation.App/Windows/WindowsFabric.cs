using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ForeignTradeContractsWorkstation.App.windows.interfaces;
using ForeignTradeContractsWorkstation.App.Windows;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.Windows.interfaces;

namespace ForeignTradeContractsWorkstation.App.windows
{
    internal static class WindowsFabric 
    {
        internal static IBaseWindowContract GetPartnerWindow()
        {
            return new WindowWrapper(new PartnerWindow());
        }
        internal static IBaseWindowContract GetUpdatePartnerWindow(Counterparties data = null)
        
        {
            return new WindowWrapper(new PartnerUpdateWindow(data));
        }
        internal static IBaseWindowContract GetDriverWindow(Driver data = null)
        {
            return new WindowWrapper(new DriveWindow());
        }
        internal static IBaseWindowContract GetUpdateDriverWindow(Driver data = null)
        {
            return new WindowWrapper(new DriverUpdateWindow(data));
        }

        internal static IBaseWindowContract GetCarWindow(Cars data = null)
        {
            return new WindowWrapper(new CarWindow());
        }
        internal static IBaseWindowContract GetUpdateCarWindow(Cars data = null)
        {
            return new WindowWrapper(new CarUpdateWindow(data));
        }

        internal static IBaseWindowContract GetCoworkerWindow(Coworker data = null)
        {
            return new WindowWrapper(new CoworkerWindow());
        }
        internal static IBaseWindowContract GetUpdateCoworkerWindow(Coworker data = null)
        {
            return new WindowWrapper(new CoworkerUpdateWindow(data));
        }

        internal static IBaseWindowContract GetStorageWindow(Storage data = null)
        {
            return new WindowWrapper(new StorageWindow());
        }
        internal static IBaseWindowContract GetUpdateStorageWindow(Storage data = null)
        {
            return new WindowWrapper(new StorageUpdateWindow(data));
        }


        internal static IBaseWindowContract GetUnitWindow(Units data = null)
        {
            return new WindowWrapper(new UnitWindow());
        }
        internal static IBaseWindowContract GetUpdateUnitWindow(Units data = null)
        {
            return new WindowWrapper(new UnitUpdateWindow(data));
        }

        internal static IBaseWindowContract GetGoodsWindow(Goods data = null)
        {
            return new WindowWrapper(new GoodsWindow());
        }

        internal static IBaseWindowContract GetUpdateGoodsWindow(Goods data = null)
        {
            return new WindowWrapper(new GoodsUpdateWindow(data));
        }


        internal static IBaseWindowContract GetOrdersLogWindow(IUpdateWindowHelper<Orders> updateHelper = null)
        {
            return new WindowWrapper(new OrdersLogWindow(updateHelper));
        }

        private class WindowWrapper : IBaseWindowContract
        {
            private Window _window;
            public WindowWrapper(Window window)
            {
                _window = window;
            }

            void IBaseWindowContract.Close()
            {
                _window.Close();
            }

            void IBaseWindowContract.Show()
            {
                _window.Show();
            }

            void IBaseWindowContract.ShowDialog()
            {
                _window.ShowDialog();
            }
        }
    }
}
