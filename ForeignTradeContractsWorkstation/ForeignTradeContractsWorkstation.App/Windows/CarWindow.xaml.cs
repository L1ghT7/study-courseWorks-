using ForeignTradeContractsWorkstation.App.Database;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.windows;
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
    /// Логика взаимодействия для CarWindow.xaml
    /// </summary>
    public partial class CarWindow : Window
    {

        public CarWindow()
        {
            InitializeComponent();
        }
        protected override void OnActivated(EventArgs e)
        {
            dataGrid.ItemsSource = WindowDataHelper.GetAllRecords<Cars>("Orders");
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WindowDataHelper.FillListboxWithDatagrid(dataGrid, listBox);
        }
        private void addRecord_Click(object sender, RoutedEventArgs e)
        {
            var form = WindowsFabric.GetUpdateCarWindow();
            form.ShowDialog();
        }
        private void updateRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem;
            WindowDataHelper.UpdateDataGridRecord<Cars>(selected, (entity) => WindowsFabric.GetUpdateCarWindow(entity));
        }
        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem;
            WindowDataHelper.DeleteDataGridRecord(selected as Cars,false, "Orders");
            dataGrid.ItemsSource = WindowDataHelper.GetAllRecords<Cars>("Orders");
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = WindowSearchHelper.Search<Cars>(searchTextBox.Text);
        }

    }
}