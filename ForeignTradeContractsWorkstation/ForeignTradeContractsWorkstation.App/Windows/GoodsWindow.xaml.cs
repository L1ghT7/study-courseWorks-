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

namespace ForeignTradeContractsWorkstation.App.Windows
{
    /// <summary>
    /// Логика взаимодействия для GoodsWindow.xaml
    /// </summary>
    public partial class GoodsWindow : Window
    {
        public GoodsWindow()
        {
            InitializeComponent();
        }
        protected override void OnActivated(EventArgs e)
        {
            var test = WindowDataHelper.GetAllRecords<Goods>("Storage");
            dataGrid.ItemsSource = test;
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WindowDataHelper.FillListboxWithDatagrid(dataGrid, listBox);
        }
        private void addRecord_Click(object sender, RoutedEventArgs e)
        {
            GoodsUpdateWindowHelper.AddGood();
        }
        private void updateRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem as Goods;

            GoodsUpdateWindowHelper.UpdateGood(selected);
        }
        private void deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem;
            WindowDataHelper.DeleteDataGridRecord<Goods>(selected as Goods);
            dataGrid.ItemsSource = WindowDataHelper.GetAllRecords<Goods>("Storage");
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = WindowSearchHelper.Search<Goods>(searchTextBox.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
