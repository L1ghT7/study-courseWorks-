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
using ForeignTradeContractsWorkstation.App.Windows.interfaces;
using ForeignTradeContractsWorkstation.App.Reports;
using iTextSharp.text.pdf;

namespace ForeignTradeContractsWorkstation.App.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrdersLogWindow.xaml
    /// </summary>
    public partial class OrdersLogWindow : Window
    {
        private readonly IUpdateWindowHelper<Orders> _helper;

        public OrdersLogWindow(IUpdateWindowHelper<Orders> helper)
        {
            _helper = helper;
            InitializeComponent();
        }
        protected override void OnActivated(EventArgs e)
        {
            dataGrid.ItemsSource = WindowDataHelper.GetAllRecords<Orders>("Cars","Driver", "Counterparties", "Coworker","Goods.Goods", "Loadig_Unloading_work");
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WindowDataHelper.FillListboxWithDatagrid(dataGrid, listBox);
        }
        private void addRecord_Click(object sender, RoutedEventArgs e)
        {
            //var form = WindowsFabric.GetUpdateGoodsWindow();
            //form.ShowDialog();
        }
        private void updateRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem as Orders;
            _helper.CurrentEntityId = selected.Id;
            _helper.InitUpdate();
           

            this.Close();
            //var selected = dataGrid.SelectedItem;
            //WindowDataHelper.UpdateDataGridRecord<Goods>(selected, (entity) => WindowsFabric.GetUpdateGoodsWindow(entity));
        }
        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem;
            WindowDataHelper.DeleteDataGridRecord(selected as Orders);
            dataGrid.ItemsSource = WindowDataHelper.GetAllRecords<Orders>("Cars", "Driver", "Counterparties", "Coworker","Goods", "Loadig_Unloading_work");
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = WindowSearchHelper.Search<Orders>(searchTextBox.Text, "Cars", "Driver", "Counterparties", "Coworker","Goods", "Loadig_Unloading_work");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
