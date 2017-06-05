using ForeignTradeContractsWorkstation.App.Database;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.Windows.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForeignTradeContractsWorkstation.App.Windows
{
    /// <summary>
    /// Логика взаимодействия для GoodsUpdateWindow.xaml
    /// </summary>
    public partial class GoodsUpdateWindow : Window
    {
        public Goods CurrentGood { get; set; }
        public IEnumerable<Storage> Storages { get; set; }

        public GoodsUpdateWindow(Goods good = null)
        {
            InitializeComponent();
            CurrentGood = good ?? new Goods();
            this.DataContext = this;
            Initialise();
        }
        private void Initialise()
        {
            Storages = WindowDataHelper.GetAllRecords<Storage>();
            storage_Combobox.ItemsSource = Storages;
            if (CurrentGood.Storage != null)
            {
                storage_Combobox.SelectedValue = CurrentGood.Storage.Id;
            }
        }


        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            var casted = (storage_Combobox.SelectedItem as Storage);
            CurrentGood.Orders = null;
            CurrentGood.Storage = casted;
            CurrentGood.storage_key = casted.Id;
            CurrentGood.Orders = null;

            if (WindowValidationHelper.ValidateEntity(casted))
            {
                WindowDataHelper.AddOrdUpdateRecord(CurrentGood);
                this.Close();
            }
        }

        private void storage_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
