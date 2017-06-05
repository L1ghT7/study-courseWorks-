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
using System.Collections.ObjectModel;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using System.Data.Entity;
using System.Collections;
using ForeignTradeContractsWorkstation.App.windows;
using ForeignTradeContractsWorkstation.App.Windows;
using ForeignTradeContractsWorkstation.App.Database;
using ForeignTradeContractsWorkstation.App.Windows.Helpers;

namespace ForeignTradeContractsWorkstation.App.windows
{
    /// <summary>
    /// Логика взаимодействия для PartnerWindow.xaml
    /// </summary>
    public partial class PartnerWindow : Window
    {
        public PartnerWindow()
        {
            InitializeComponent();
        }
        protected override void OnActivated(EventArgs e)
        {
            dataGrid.ItemsSource = WindowDataHelper.GetAllRecords<Counterparties>("Orders");
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WindowDataHelper.FillListboxWithDatagrid(dataGrid, listBox);
        }
        private void addRecord_Click(object sender, RoutedEventArgs e)
        {
            var form = WindowsFabric.GetUpdatePartnerWindow();
            form.ShowDialog();
        }
        private void updateRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem;
            WindowDataHelper.UpdateDataGridRecord<Counterparties>(selected, (entity) => WindowsFabric.GetUpdatePartnerWindow(entity));
        }
        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem;
            WindowDataHelper.DeleteDataGridRecord<Counterparties>(selected as Counterparties,false,"Orders");
            dataGrid.ItemsSource = WindowDataHelper.GetAllRecords<Counterparties>("Orders");
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = WindowSearchHelper.Search<Counterparties>(searchTextBox.Text, "Orders");
        }
    }
}
