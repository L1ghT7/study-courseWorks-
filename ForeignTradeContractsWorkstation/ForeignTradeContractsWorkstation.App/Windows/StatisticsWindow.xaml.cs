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
using ForeignTradeContractsWorkstation.App.Windows.Helpers.Charts;
using LiveCharts;
using LiveCharts.Wpf;

namespace ForeignTradeContractsWorkstation.App.Windows
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow()
        {
            InitializeComponent();

            
            
            ProductsChartHelper.DrawProductsChart(PieChartExampleTest,fromPicker.SelectedDate,toPicker.SelectedDate,5);
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductsChartHelper.DrawProductsChart(PieChartExampleTest, fromPicker.SelectedDate, toPicker.SelectedDate,5);
        }

        

        private void numberPicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {


            ProductsChartHelper.DrawProductsChart(PieChartExampleTest, fromPicker.SelectedDate, toPicker.SelectedDate, Convert.ToInt32(numberPicker.Text));
        }
    }


   
}

