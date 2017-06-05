using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ForeignTradeContractsWorkstation.App.Windows.Helpers
{
    public static class GoodsUpdateWindowHelper
    {
        private static string _updateWarningMessage = "Добавьте хоты бы одно место хранение";

        public static void UpdateGood(Goods selected)
        {
            var storages = WindowDataHelper.GetAllRecords<Storage>();
            if (storages.Count == 0)
            {
                MessageBox.Show("Добавьте хоты бы одно место хранение!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            WindowDataHelper.UpdateDataGridRecord<Goods>(selected, (entity) => WindowsFabric.GetUpdateGoodsWindow(entity));

        }

        public static void AddGood()
        {
            var storages = WindowDataHelper.GetAllRecords<Storage>();
            if (storages.Count == 0)
            {
                MessageBox.Show("Добавьте хоты бы одно место хранение!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var form = WindowsFabric.GetUpdateGoodsWindow();
            form.ShowDialog();

        }


    }
}
