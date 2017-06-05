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
    /// Логика взаимодействия для UnitUpdateWindow.xaml
    /// </summary>
    public partial class UnitUpdateWindow : Window
    {
        public Units CurrentUnit { get; set; }
        public UnitUpdateWindow(Units unit = null)
        {
            InitializeComponent();
            CurrentUnit = unit ?? new Units();
            this.DataContext = CurrentUnit;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowDataHelper.AddOrdUpdateRecord(CurrentUnit);
            this.Close();
        }
    }
}
