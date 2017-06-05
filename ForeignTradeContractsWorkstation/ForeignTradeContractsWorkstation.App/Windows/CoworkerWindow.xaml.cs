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
    /// Логика взаимодействия для CoworkerWindow.xaml
    /// </summary>
    public partial class CoworkerWindow : Window
    {
        public CoworkerWindow()
        {
            InitializeComponent();
        }
        protected override void OnActivated(EventArgs e)
        {
            dataGrid.ItemsSource = WindowDataHelper.GetAllRecords<Coworker>("Orders");
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WindowDataHelper.FillListboxWithDatagrid(dataGrid, listBox);
        }
        private void addRecord_Click(object sender, RoutedEventArgs e)
        {
            var form = WindowsFabric.GetUpdateCoworkerWindow();
            form.ShowDialog();
        }
        private void updateRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem;
            WindowDataHelper.UpdateDataGridRecord<Coworker>(selected, (entity) => WindowsFabric.GetUpdateCoworkerWindow(entity));
        }
        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem;
            WindowDataHelper.DeleteDataGridRecord<Coworker>(selected as Coworker,false, "Orders");
            dataGrid.ItemsSource = WindowDataHelper.GetAllRecords<Coworker>("Orders");
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = WindowSearchHelper.Search<Coworker>(searchTextBox.Text);
        }
    }
}
