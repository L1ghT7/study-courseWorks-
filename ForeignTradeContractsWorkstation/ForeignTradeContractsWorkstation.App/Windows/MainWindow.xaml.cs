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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ForeignTradeContractsWorkstation.App.windows;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.Database;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using ForeignTradeContractsWorkstation.App.Windows.Helpers;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory.Entities;
using ForeignTradeContractsWorkstation.App.Reports;
using ForeignTradeContractsWorkstation.App.Windows;
using ForeignTradeContractsWorkstation.App.Windows.interfaces;
using Xceed.Wpf.Toolkit;

namespace ForeignTradeContractsWorkstation.App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IUpdateWindowHelper<Orders>
    {
        public IEnumerable<Storage> Storages;
        public Loadig_Unloading_work LoadFirst;
        public Loadig_Unloading_work LoadSecond;


        public Orders CurrentOrder { get; set; }
        public ICollection<Counterparties> Counterparties;
        public List<Goods> GoodsForOrder;
        public ICollection<Coworker> Coworkers;

        public ICollection<Driver> Drivers1;

        public ICollection<Cars> Cars;
        public Storage CurrentStorage;

        public long CurrentOrderId { get; set; }

        public long? CurrentEntityId
        {
            get;
            set;
        }

        public MainWindow()
        {
            Initialize();
            InitializeComponent();
        }
        private void Initialize()
        {

        }
        private void OrdersLogMenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            var ordersLogWindow = WindowsFabric.GetOrdersLogWindow(this);
            ordersLogWindow.ShowDialog();
        }

        private void StatisticMenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            var a = new StatisticsWindow();
            a.ShowDialog();
        }

        private void PartnerMenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            var parnterWindow = WindowsFabric.GetPartnerWindow();
            parnterWindow.ShowDialog();
        }
        private void DriverMenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            var driverWindow = WindowsFabric.GetDriverWindow();
            driverWindow.ShowDialog();
        }
        private void CarMenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            var carWindow = WindowsFabric.GetCarWindow();
            carWindow.ShowDialog();
        }
        private void CoworkerMenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            var carWindow = WindowsFabric.GetCoworkerWindow();
            carWindow.ShowDialog();
        }
        private void StorageMenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            var storageWindow = WindowsFabric.GetStorageWindow();
            storageWindow.ShowDialog();
        }
        private void UnitMenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            //var unitWindow = WindowsFabric.GetUnitWindow();
            //unitWindow.ShowDialog();
        }
        private void GoodsMenuItemClickHandler(object sender, RoutedEventArgs e)
        {
            var unitWindow = WindowsFabric.GetGoodsWindow();
            unitWindow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            LoadDefaultData();
        }
        private void LoadDefaultData()
        {

            Storages = WindowDataHelper.GetAllRecords<Storage>();
            Counterparties = WindowDataHelper.GetAllRecords<Counterparties>();
            GoodsForOrder = goodsDataGrid.Items.Count != 0 ? (goodsDataGrid.ItemsSource as List<Goods>) : new List<Goods>();
            Coworkers = WindowDataHelper.GetAllRecords<Coworker>();

            Drivers1 = WindowDataHelper.GetAllRecords<Driver>();
            Cars = WindowDataHelper.GetAllRecords<Cars>();

            drivers1_combobox.ItemsSource = Drivers1;


            coworkers_combobox.ItemsSource = Coworkers;
            cars_combobox.ItemsSource = Cars;
            counterParties_combobox.ItemsSource = Counterparties;
            allStorages.ItemsSource = Storages;
            var firstStorage = Storages.FirstOrDefault();
            if (firstStorage != null)
            {
                goodCombobox.ItemsSource = WindowDataHelper.GetAllRecords<Goods>().Where(x => x.storage_key == firstStorage.Id);
            }
            CurrentStorage = new Storage();
            if (CurrentEntityId.HasValue)
            {
                UpdateEntity();
            }
            CalculateAllSum();
        }

        private void CreateNewOrder(object sender, RoutedEventArgs e)
        {
            CurrentEntityId = null;
            LoadDefaultData();

            number_textbox.Text = string.Empty;
            ser_textbox.Text = string.Empty;
            date_datepicke.DisplayDate = DateTime.Now;
            cars_combobox.SelectedValue = 0;
            counterParties_combobox.SelectedValue = 0;
            coworkers_combobox.SelectedValue = 0;
            drivers1_combobox.SelectedValue = 0;
            GoodsForOrder = new List<Goods>();
            goodsDataGrid.ItemsSource = GoodsForOrder;
        }

        private void allStorages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            var selectedItem = combobox.SelectedItem as Storage;
            if (selectedItem != null)
            {
                goodCombobox.ItemsSource = WindowDataHelper.GetAllRecords<Goods>().Where(x => x.storage_key == selectedItem.Id);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void counterParties_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = counterParties_combobox.SelectedItem as Counterparties;
            if (selectedItem != null)
            {
                mainContract_textbox.Text = selectedItem.main_contract;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = goodCombobox.SelectedItem as Goods;



            if (selectedItem != null)
            {

                var item = GoodsForOrder.FirstOrDefault(x => x.Id == selectedItem.Id);
                if (item == null)
                {
                    var amount = Convert.ToDecimal(numberPicker.Text);
                    var sum = amount * (decimal)selectedItem.accounting_price.Value;
                    selectedItem.TotalSum += (decimal)sum;
                    selectedItem.TotalAmount += amount;

                    GoodsForOrder.Add(selectedItem);
                    goodsDataGrid.ItemsSource = null;
                    goodsDataGrid.ItemsSource = GoodsForOrder;

                    total_sum.Text = selectedItem.TotalSum + " Р.";
                    total_weight.Text = selectedItem.TotalAmount * (decimal)selectedItem.the_weight.Value + " КГ.";
                }
                else
                {
                    var amount = Convert.ToDecimal(numberPicker.Text);
                    var sum = amount * (decimal)item.accounting_price.Value;
                    item.TotalSum += (decimal)sum;
                    item.TotalAmount += amount;
                    total_sum.Text = item.TotalSum + " Р.";
                    total_weight.Text = item.TotalAmount * (decimal)item.the_weight.Value + " КГ.";

                }
            }
            CalculateAllSum();
        }

        private void button_Click11(object sender, RoutedEventArgs e)
        {
            var selected = goodsDataGrid.SelectedItem as Goods;

            if (selected != null)
            {
                GoodsForOrder.Remove(selected);
                total_sum.Text = string.Empty;
                total_weight.Text = string.Empty;
                selected.TotalAmount = 0;
                selected.TotalSum = 0;
            }
            goodsDataGrid.ItemsSource = null;
            goodsDataGrid.ItemsSource = GoodsForOrder;
            CalculateAllSum();

        }

        private void cars_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCar = cars_combobox.SelectedItem as Cars;
            trailer_textbox.Text = selectedCar?.trailer;
        }


        private void CreateAddConfig()
        {
            Orders order = new Orders();
            order.Id = CurrentEntityId.HasValue ? CurrentEntityId.Value : 0;
            order.consignment_number = number_textbox.Text;
            order.consignment_series = ser_textbox.Text;
            order.date_of_order = date_datepicke.DisplayDate;

            order.car_key = (cars_combobox.SelectedItem as Cars)?.Id;
            order.Counterparties_key = (counterParties_combobox.SelectedItem as Counterparties)?.Id;
            order.coworker_key = (coworkers_combobox.SelectionBoxItem as Coworker)?.Id;
            order.driver_key = (drivers1_combobox.SelectedItem as Driver)?.Id;

            var data = GoodsForOrder.Select((x) => new OrdersGoods { GoodsId = x.Id, OrderId = order.Id, Id = x.Id, TotalSum = x.TotalSum, TotalAmount = x.TotalAmount }).ToList();
            order.Goods = data;


            order.VAT = Convert.ToByte(percentPicker.Text);
            order.TotalSum = Convert.ToDecimal(Regex.Match(sumLabel.Content as string, @"\d+").Value);

            Loadig_Unloading_work first = new Loadig_Unloading_work()
            {
                way = way1.Text,
                executor = load1.Text,
                arrival_date = DateArrival1.DisplayDate,
                arrival_time = TimeArrival1.Value.Value.TimeOfDay,
                date_of_departure = DepartureDate1.DisplayDate,
                departure_time = TimeDeparture1.Value.Value.TimeOfDay,
                addres = Address1.Text
            };
            Loadig_Unloading_work second = new Loadig_Unloading_work()
            {
                way = way2.Text,
                executor = load2.Text,
                arrival_date = DateArrival2.DisplayDate,
                arrival_time = TimeArrival2.Value.Value.TimeOfDay,
                date_of_departure = DepartureDate2.DisplayDate,
                departure_time = TimeDeparture2.Value.Value.TimeOfDay,
                addres = Address2.Text
            };

            order.Loadig_Unloading_work.Add(first);
            order.Loadig_Unloading_work.Add(second);



            if (CurrentEntityId.HasValue)
            {
                CurrentEntityId = WindowDataHelper.UpdateFullGraph(order)?.Id;
           
            }
            else
            {
                WindowDataHelper.AddFullGraph(order);
             
            }

        }
        private void UpdateEntity()
        {
            if (!CurrentEntityId.HasValue) { return; }
            var include = new string[] { "Cars", "Driver", "Counterparties", "Coworker", "Goods.Goods", "Loadig_Unloading_work" };
            var allEntities = WindowDataHelper.GetAllRecords<Orders>(include);
            var currentOrder = allEntities.FirstOrDefault(x => x.Id.Equals(CurrentEntityId));
            CurrentOrder = currentOrder;
            MapOrderToViewModel(currentOrder);

        }
        private void MapOrderToViewModel(Orders order)
        {
            if (order == null) { return; }

            number_textbox.Text = order.consignment_number;
            ser_textbox.Text = order.consignment_series;
            date_datepicke.DisplayDate = order.date_of_order.Value;
            cars_combobox.SelectedValue = order.car_key;
            counterParties_combobox.SelectedValue = order.Counterparties_key;
            coworkers_combobox.SelectedValue = order.coworker_key;
            drivers1_combobox.SelectedValue = order.driver_key;
            GoodsForOrder = new List<Goods>();

            percentPicker.Text = order.VAT.ToString();
            sumLabel.Content = order.TotalSum.ToString();


            Func<OrdersGoods, Goods> selector = (orderGoods) =>
            {
                orderGoods.Goods.TotalAmount = orderGoods.TotalAmount;
                orderGoods.Goods.TotalSum = orderGoods.TotalSum;
                return orderGoods.Goods;
            };

            GoodsForOrder.AddRange(order.Goods.Select(selector));
            goodsDataGrid.ItemsSource = GoodsForOrder;


            var first = order.Loadig_Unloading_work.ToList()[0];
            var second = order.Loadig_Unloading_work.ToList()[1];


            TimeArrival1.Value = new DateTime().Add(first.arrival_time.Value);
            TimeDeparture1.Value = new DateTime().Add(first.departure_time.Value);

            way1.Text = first.way;
            load1.Text = first.executor;
            DateArrival1.SelectedDate = first.arrival_date.Value;
            DepartureDate1.SelectedDate = first.date_of_departure.Value;
            Address1.Text = first.addres;


            TimeArrival2.Value = new DateTime().Add(second.arrival_time.Value);
            TimeDeparture2.Value = new DateTime().Add(second.departure_time.Value);

            way2.Text = second.way;
            load2.Text = second.executor;
            DateArrival2.SelectedDate = second.arrival_date.Value;
            DepartureDate2.SelectedDate = second.date_of_departure.Value;
            Address2.Text = second.addres;
        }

        private void coworkers_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCoworker = coworkers_combobox.SelectedItem as Coworker;
            sender_textbox.Text = selectedCoworker?.full_name;
        }

        private void drivers1_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDriver = drivers1_combobox.SelectedItem as Driver;
            driver_textbox.Text = selectedDriver?.full_name;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            CreateAddConfig();
        }

        private void CalculateAllSum()
        {
            decimal precent = 0;
            if (!string.IsNullOrEmpty(percentPicker.Text))
            {
                precent = Convert.ToDecimal(percentPicker.Text);
            }
            var goods = (IEnumerable<Goods>)goodsDataGrid.ItemsSource;
            decimal totalSum = 0;
            if (goods != null)
            {
                foreach (var product in goods)
                {
                    decimal taxToPrice = 0;
                    if (product.VAT.HasValue)
                    {
                        taxToPrice = (product.TotalSum / 100) * (decimal)product.VAT.Value;
                    }
                    totalSum += product.TotalSum + taxToPrice;
                }
                if (precent != 0)
                {
                    totalSum += (totalSum / 100) * precent;
                }
                sumLabel.Content = $"{totalSum} Р.";
                taxLabel.Content = $"{precent} %";
            }
        }

        private void goodsDataGrid_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void goodsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var goods = goodsDataGrid.ItemsSource as IEnumerable<Goods>;
        }

        private void goodsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var goods = goodsDataGrid.ItemsSource as IEnumerable<Goods>;
        }

        public void Update(Orders entity)
        {
            number_textbox.Text = entity.consignment_number;
        }

        private void goodsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = goodsDataGrid.SelectedItem as Goods;
            if (selected != null)
            {
                total_sum.Text = selected.TotalSum + " Р.";
                total_weight.Text = selected.TotalAmount * (decimal)selected.the_weight.Value + " КГ.";

                numberPicker.Text = selected.TotalAmount.ToString();

            }
        }

        private void percentPicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CalculateAllSum();
        }


        public void PrintTTNVertical(object sender, RoutedEventArgs e)
        {
            var config = GetPrintConfig();

            var taskFactory = new TaskFactory();
            taskFactory.StartNew(() =>
            {

                var pathToFile = new TTNExcelReport("ttn-vertical.xlsx","first").CreateReport(config);
            
                PrinterHelper.ShowExcel(pathToFile);
            });
        }


        public void PrintInvoiceReport(object sender, RoutedEventArgs e)
        {
            var config = GetPrintConfig();

            var taskFactory = new TaskFactory();
            taskFactory.StartNew(() =>
            {

                var pathToFile = new InvoiceWordReport("Invoice.docx").CreateReport(config);
                PrinterHelper.ShowWord(pathToFile);
            });
        }

        public void PrintTTNVerticalWithAttachment(object sender, RoutedEventArgs e)
        {

        }

        public void PrintTTNHorizontal(object sender, RoutedEventArgs e)
        {
            var config = GetPrintConfig();
            var taskFactory = new TaskFactory();
            taskFactory.StartNew(() =>
            {

                var pathToFile = new TTNHorizontalExcelReport("ttn-horizontal.xlsx","first").CreateReport(config);

                PrinterHelper.ShowExcel(pathToFile);
            });
        }

        public void PrintTTNHorizontalWithAttachment(object sender, RoutedEventArgs e)
        {

        }


        public Orders GetPrintConfig()
        {
            Orders order = new Orders();
            order.Id = CurrentEntityId.HasValue ? CurrentEntityId.Value : 0;
            order.consignment_number = number_textbox.Text;
            order.consignment_series = ser_textbox.Text;
            order.date_of_order = date_datepicke.DisplayDate;

            order.Cars = cars_combobox.SelectedItem as Cars;
            order.Counterparties = counterParties_combobox.SelectedItem as Counterparties;
            order.Coworker =coworkers_combobox.SelectionBoxItem as Coworker;
            order.Driver = drivers1_combobox.SelectedItem as Driver;

            var data = GoodsForOrder.Select((x) => new OrdersGoods {Goods = x, GoodsId = x.Id, OrderId = order.Id, Id = x.Id, TotalSum = x.TotalSum, TotalAmount = x.TotalAmount }).ToList();
            order.Goods = data;


            order.VAT = Convert.ToByte(percentPicker.Text);
            order.TotalSum = Convert.ToDecimal(Regex.Match(sumLabel.Content as string, @"\d+").Value);

            Loadig_Unloading_work first = new Loadig_Unloading_work()
            {
                way = way1.Text,
                executor = load1.Text,
                arrival_date = DateArrival1.DisplayDate,
                arrival_time = TimeArrival1.Value.Value.TimeOfDay,
                date_of_departure = DepartureDate1.DisplayDate,
                departure_time = TimeDeparture1.Value.Value.TimeOfDay,
                addres = Address1.Text
            };
            Loadig_Unloading_work second = new Loadig_Unloading_work()
            {
                way = way2.Text,
                executor = load2.Text,
                arrival_date = DateArrival2.DisplayDate,
                arrival_time = TimeArrival2.Value.Value.TimeOfDay,
                date_of_departure = DepartureDate2.DisplayDate,
                departure_time = TimeDeparture2.Value.Value.TimeOfDay,
                addres = Address2.Text
            };

            order.Loadig_Unloading_work.Add(first);
            order.Loadig_Unloading_work.Add(second);

            order.ToListNumber = textBox2223_Copy.Text;
            order.ShipCustomer = textBox2223_Copy3.Text;
            order.LeaveBases = textBox1_Copy2.Text;
            order.PointLoading = comboBox_Copy3.Text;
            order.PointUnloading = textBox2223_Copy4.Text;
            order.ProxyNumber = textBox22.Text;
            order.ProxyDate = spe.Text;
            order.ProxyGiver = textBox12_Copy.Text;




            return order;
        }

    }
}
