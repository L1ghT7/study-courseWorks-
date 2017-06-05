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
    /// Логика взаимодействия для CoworkerUpdateWindow.xaml
    /// </summary>
    public partial class CoworkerUpdateWindow : Window
    {
        public string[] Sex = { "мужской", "женский" };
        public Coworker CurrentСoworker { get; set; }
        public CoworkerUpdateWindow(Coworker coworker = null)
        {
            InitializeComponent();
            CurrentСoworker = coworker ?? new Coworker() { sex=Sex.First()};
            sex.ItemsSource = Sex;
            this.DataContext = CurrentСoworker;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowValidationHelper.ValidateEntity(CurrentСoworker))
            {
                WindowDataHelper.AddOrdUpdateRecord(CurrentСoworker);
                this.Close();
            }
        }
    }
}
