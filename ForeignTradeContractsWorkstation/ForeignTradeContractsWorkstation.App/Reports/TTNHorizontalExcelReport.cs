using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory.Entities;
using ForeignTradeContractsWorkstation.App.Reports.base_classes;
using ForeignTradeContractsWorkstation.App.Reports.Helpers;
using SpreadsheetLight;

namespace ForeignTradeContractsWorkstation.App.Reports
{
    public class TTNHorizontalExcelReport : BaseExcelReport<Orders>
    {
        public TTNHorizontalExcelReport(string templateName,string sheetName)
            : base(templateName,sheetName)
        {

        }

        protected override SLDocument sl { get; set; }

        public override string CreateReport(Orders viewModel)
        {
            sl = new SLDocument(FullPathToTemplate, SheetName);

            CellOffset = -2;

            SetValue("C", 16, DateTime.Now.Day.ToString());
            SetValue("T", 16, DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture));
            SetValue("AJ", 16, (DateTime.Now.Year.ToString().Substring(2)));
            SetValue("BF", 6, viewModel.Counterparties?.CBU);
            SetValue("BP", 6, viewModel.Counterparties?.CBU);
            SetValue("CD", 6, viewModel.Counterparties?.CBU);
            SetValue("AK", 17, viewModel.Cars?.type_of_car);
            SetValue("DA", 17, viewModel.Cars?.trailer);
            SetValue("AW", 21, viewModel.Driver?.full_name);

            SetValue("R", 27, $"{viewModel.Counterparties?.full_name},{viewModel.Counterparties?.legal_address}");
            SetValue("R", 29, $"{viewModel.Counterparties?.full_name},{viewModel.Counterparties?.legal_address}");
            SetValue("AW", 31, viewModel.Counterparties?.main_contract);

            //additionals fields 
            SetValue("U", 19, viewModel.ToListNumber);

            SetValue("AS", 24, viewModel.ShipCustomer);

            SetValue("Q", 31, viewModel.LeaveBases);
            SetValue("Q", 33, viewModel.PointLoading);
            SetValue("CI", 33, viewModel.PointUnloading);
            SetValue("BX", 57, $"{ viewModel.ProxyNumber} {viewModel.ProxyDate}");


            SetValue("BS", 59, viewModel.ProxyGiver);
            SetValue("CD", 61, viewModel.AcceptedGuy);


            CellOffset = 0;
            int start = 42;
            
            var list = viewModel.Goods.ToList();

            int totalAmount = 0;
            float? totalCost = 0;
            float? totalPrice = 0;
            float? totalVat = 0;

            float? totalVatSum = 0;
            float? totalTotalPrice = 0;
            decimal totalWeight = 0;
            
            foreach (var index in AddDynamicRows(42, list.Count))
            {
               OrdersGoods node = null;

                if (list.Count != 0)
                {
                    node = list[index - start];
                }

                if (node != null)
                {
                    SetValue("A", index, node.Goods?.full_name);
                    SetValue("AN", index, "КГ.");

                    totalAmount += (int)node.TotalAmount;
                    totalCost += node.Goods.purchase_price;
                    totalPrice += (node.Goods.purchase_price * (float)node.TotalAmount);
                    totalWeight += (node.TotalAmount * (decimal)node.Goods.the_weight);

                    var weight = (int)node.TotalAmount * (int)node.Goods?.the_weight;

                    SetValue("AW", index,
                       ((int)node.TotalAmount).ToString(CultureInfo.InvariantCulture));
                    SetValue("BG", index, GetMoneyString(node.Goods.purchase_price));

                    SetValue("BS", index, GetMoneyString(node.Goods?.purchase_price * (float)node.TotalAmount));

                    totalVat += node.Goods?.VAT.Value;

                    SetValue("CF", index, GetWithPrecent(node.Goods?.VAT.Value));

                    var withVAT = ((node.Goods?.purchase_price.Value * (float)node.TotalAmount) / 100) * node.Goods.VAT;
                    totalVatSum += withVAT;


                    SetValue("CO", index, GetMoneyString(withVAT.Value));
                    totalTotalPrice += ((node.Goods.purchase_price * (float)node.TotalAmount) + withVAT);
                    SetValue("DB", index, GetMoneyString((node.Goods.purchase_price * (float)node.TotalAmount) + withVAT));

                    SetValue("DZ", index, weight.ToString());
                }


            }
           
            SetValue("AW", 43, totalAmount.ToString());
            SetValue("BG", 43, GetMoneyString(totalCost));
            SetValue("BS", 43, GetMoneyString(totalPrice));
            //SetValue("BX", 44, GetWithPrecent(totalVat));

            SetValue("CO", 43, GetMoneyString(totalVatSum));
            SetValue("DB", 43, GetMoneyString(totalTotalPrice));
            SetValue("DZ", 43, ((int)totalWeight).ToString());

            //SetValue("AW", 33, viewModel.); why???


            var totalVatSumInWords = NumberToWordConverterHelper.ConvertCurrencyToWords((decimal)totalVatSum);
            SetValue("AW", 46, $"{totalVatSumInWords.Number} ({(int)totalVatSum})");
            SetValue("DJ", 46, totalVatSumInWords.Coins);

            var totalTotalPriceInWords = NumberToWordConverterHelper.ConvertCurrencyToWords((decimal)totalTotalPrice);
            SetValue("AW", 49, $"{totalTotalPriceInWords.Number} ({(int)totalTotalPrice})");
            SetValue("DJ", 49, totalTotalPriceInWords.Coins);



            sl.SelectWorksheet("second");
            CellOffset = 0;

            var totalWeightInWord = NumberToWordConverterHelper.ConvertCurrencyToWords(totalWeight);
            SetValue("AJ", 1, $"{totalWeightInWord.Number} ({(int)totalWeight}) КГ.");

            SetValue("S", 3, $"{viewModel.Coworker.full_name},{viewModel.Coworker.position}");
            SetValue("S", 3, $"{viewModel.Coworker.full_name},{viewModel.Coworker.position}");
            SetValue("CW", 3, viewModel.Driver?.full_name);


            var first = viewModel.Loadig_Unloading_work.ToList()[0];
            var second = viewModel.Loadig_Unloading_work.ToList()[1];

            SetValue("K", 24, first.executor);
            SetValue("AA", 24, first.way);

            SetValue("AN", 24, first.Id.ToString());

            SetValue("AU", 24, GetFormatedDateAndTime(first.arrival_date, first.arrival_time));
            SetValue("BD", 24, GetFormatedDateAndTime(first.arrival_date, first.arrival_time));
            SetValue("BM", 24, first.addres);



            SetValue("K", 25, second.executor);
            SetValue("AA", 25, second.way);

            SetValue("AN", 25, second.Id.ToString());

            SetValue("AU", 25, GetFormatedDateAndTime(second.arrival_date, second.arrival_time));
            SetValue("BD", 25, GetFormatedDateAndTime(second.date_of_departure, first.departure_time));
            SetValue("BM", 25, second.addres);





            sl.SelectWorksheet("thrid");
            start = 7;
            list = viewModel.Goods.ToList();

            totalAmount = 0;
            totalCost = 0;
            totalPrice = 0;
            totalVat = 0;

            totalVatSum = 0;
            totalTotalPrice = 0;
            totalWeight = 0;


            CellOffset = 0;
            foreach (var index in AddDynamicRows(7, list.Count))
            {
                OrdersGoods node = null;

                if (list.Count != 0)
                {
                    node = list[index - start];
                }

                if (node != null)
                {
                    SetValue("A", index, node.Goods?.full_name);
                    SetValue("B", index, "КГ.");

                    totalAmount += (int)node.TotalAmount;
                    totalCost += node.Goods.purchase_price;
                    totalPrice += node.Goods.purchase_price;
                    totalWeight += (node.TotalAmount * (decimal)node.Goods.the_weight);

                    var weight = (int)node.TotalAmount * (int)node.Goods?.the_weight;

                    SetValue("C", index,
                        ((int)node.TotalAmount).ToString(CultureInfo.InvariantCulture));
                    SetValue("D", index, GetMoneyString(node.Goods.purchase_price));

                    SetValue("E", index, GetMoneyString(node.Goods?.purchase_price * (float)node.TotalAmount));

                    totalVat += node.Goods?.VAT.Value;

                    SetValue("F", index, GetWithPrecent(node.Goods?.VAT.Value));

                    var withVAT = ((node.Goods?.purchase_price.Value * (float)node.TotalAmount) / 100) * node.Goods.VAT;
                    totalVatSum += withVAT;


                    SetValue("G", index, GetMoneyString(withVAT.Value));
                    totalTotalPrice += ((node.Goods.purchase_price * (float)node.TotalAmount) + withVAT);
                    SetValue("H", index, GetMoneyString((node.Goods.purchase_price * (float)node.TotalAmount) + withVAT));

                    SetValue("J", index, weight.ToString());
                }
            }

            start++;
            SetValue("C", start, totalAmount.ToString());
            SetValue("D", start, GetMoneyString(totalCost));
            SetValue("E", start, GetMoneyString(totalPrice));
            //SetValue("BX", 44, GetWithPrecent(totalVat));

            SetValue("G", start, GetMoneyString(totalVatSum));
            SetValue("H", start, GetMoneyString(totalTotalPrice));
            SetValue("J", start, ((int)totalWeight).ToString());


            SaveReport();
            return FullDestinationPath;
        }
    }
}
