using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory.Entities;
using ForeignTradeContractsWorkstation.App.Reports.base_classes;
using ForeignTradeContractsWorkstation.App.Reports.Helpers;
using SpreadsheetLight;
using TsSoft.Orthography.Numbers;
using Xceed.Wpf.Toolkit;
using static iTextSharp.text.Font;
using HorizontalAlignmentValues = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues;

namespace ForeignTradeContractsWorkstation.App.Reports
{
    public sealed class TTNExcelReport : BaseExcelReport<Orders>
    {


        public TTNExcelReport(string templateName,string sheetName)
            : base(templateName,sheetName)
        {

        }

        protected override SLDocument sl { get; set; }



        public override string CreateReport(Orders viewModel)
        {


            sl = new SLDocument(FullPathToTemplate, SheetName);
            SetValue("C", 16, DateTime.Now.Day.ToString());
            SetValue("T", 16, DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture));
            SetValue("AJ", 16, (DateTime.Now.Year.ToString().Substring(2)));
            SetValue("AU", 4, viewModel.Counterparties?.CBU);
            SetValue("AY", 4, viewModel.Counterparties?.UNP);
            SetValue("BL", 4, viewModel.Counterparties?.UNP);
            SetValue("AK", 17, viewModel.Cars?.type_of_car);
            SetValue("DA", 17, viewModel.Cars?.trailer);
            SetValue("AW", 21, viewModel.Driver?.full_name);

            SetValue("R", 27, $"{viewModel.Counterparties?.full_name},{viewModel.Counterparties?.legal_address}");
            SetValue("R", 29, $"{viewModel.Counterparties?.full_name},{viewModel.Counterparties?.legal_address}");
            //SetValue("AW", 31, viewModel.Counterparties?.main_contract);


            //additionals fields 
            SetValue("U", 19,viewModel.ToListNumber);

            SetValue("AS", 24, viewModel.ShipCustomer);

            SetValue("Q", 31, viewModel.LeaveBases);
            SetValue("Q", 33, viewModel.PointLoading);
            SetValue("CI", 33, viewModel.PointUnloading);
            SetValue("BX", 57,$"{ viewModel.ProxyNumber} {viewModel.ProxyDate}");

       
            SetValue("BS", 59, viewModel.ProxyGiver);
            SetValue("CD", 61, viewModel.AcceptedGuy);

        


            int start = 43;
            var list = viewModel.Goods.ToList();

            int totalAmount = 0;
            float? totalCost = 0;
            float? totalPrice = 0;
            float? totalVat = 0;

            float? totalVatSum = 0;
            float? totalTotalPrice = 0;
            decimal totalWeight = 0;

            foreach (var index in AddDynamicRows(43, list.Count))
            {
                OrdersGoods node = null;

                if (list.Count != 0)
                {
                    node = list[index - start];
                }

                if (node != null)
                {
                    SetValue("A", index, node.Goods?.full_name);
                    SetValue("AF", index, "КГ.");

                    totalAmount += (int) node.TotalAmount;
                    totalCost += node.Goods.purchase_price;
                    totalPrice += (node.Goods.purchase_price * (float)node.TotalAmount);
                    totalWeight += (node.TotalAmount * (decimal)node.Goods.the_weight);

                    var weight = (int)node.TotalAmount * (int)node.Goods?.the_weight;

                    SetValue("AO", index,
                         ((int)node.TotalAmount).ToString(CultureInfo.InvariantCulture));
                    SetValue("AX", index, GetMoneyString(node.Goods.purchase_price));

                    SetValue("BK", index, GetMoneyString(node.Goods?.purchase_price * (float)node.TotalAmount));

                    totalVat += node.Goods?.VAT.Value;

                    SetValue("BW", index, GetWithPrecent(node.Goods?.VAT.Value));

                    var withVAT = ((node.Goods?.purchase_price.Value * (float)node.TotalAmount) / 100) * node.Goods.VAT;
                    totalVatSum += withVAT;


                    SetValue("CD", index, GetMoneyString(withVAT.Value));
                    totalTotalPrice += ((node.Goods.purchase_price * (float)node.TotalAmount) + withVAT);
                    SetValue("CO", index, GetMoneyString((node.Goods.purchase_price * (float)node.TotalAmount) + withVAT));

                    SetValue("DJ", index, weight.ToString());
                }


            }

            SetValue("AO", 44, totalAmount.ToString());
            SetValue("AX", 44, GetMoneyString(totalCost));
            SetValue("BK", 44, GetMoneyString(totalPrice));
            //SetValue("BX", 44, GetWithPrecent(totalVat));

            SetValue("CD", 44, GetMoneyString(totalVatSum));
            SetValue("CO", 44, GetMoneyString(totalTotalPrice));
            SetValue("DJ", 44, ((int)totalWeight).ToString());

            //SetValue("AW", 33, viewModel.); why???


            var totalVatSumInWords = NumberToWordConverterHelper.ConvertCurrencyToWords((decimal)totalVatSum);
            SetValue("AW", 46, $"{totalVatSumInWords.Number} ({(int)totalVatSum})");
            SetValue("DJ", 46, totalVatSumInWords.Coins);

            var totalTotalPriceInWords = NumberToWordConverterHelper.ConvertCurrencyToWords((decimal)totalTotalPrice);
            SetValue("AW", 49, $"{totalTotalPriceInWords.Number} ({(int)totalTotalPrice})");
            SetValue("DJ", 49, totalTotalPriceInWords.Coins);

            var totalWeightInWord = NumberToWordConverterHelper.ConvertCurrencyToWords(totalWeight);
            SetValue("AJ", 51, $"{totalWeightInWord.Number} ({(int)totalWeight}) КГ.");

            SetValue("S", 53, $"{viewModel.Coworker.full_name},{viewModel.Coworker.position}");
            SetValue("S", 57, $"{viewModel.Coworker.full_name},{viewModel.Coworker.position}");
            SetValue("CK", 53, viewModel.Driver?.full_name);


            var first = viewModel.Loadig_Unloading_work.ToList()[0];
            var second = viewModel.Loadig_Unloading_work.ToList()[1];

            SetValue("I", 77, first.executor);
            SetValue("AC", 77, first.way);

            SetValue("AO", 77, first.Id.ToString());



            SetValue("AU", 77, GetFormatedDateAndTime(first.arrival_date, first.arrival_time));
            SetValue("BE", 77, GetFormatedDateAndTime(first.date_of_departure, first.departure_time));
            SetValue("BO", 77, first.addres);



            SetValue("I", 78, second.executor);
            SetValue("AC", 78, second.way);

            SetValue("AO", 78, second.Id.ToString());

            SetValue("AU", 78, GetFormatedDateAndTime(second.arrival_date, second.arrival_time));
            SetValue("BE", 78, GetFormatedDateAndTime(second.date_of_departure, first.departure_time));
            SetValue("BO", 78, second.addres);





            sl.SelectWorksheet("second");
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
                    totalPrice += (node.Goods.purchase_price * (float)node.TotalAmount);
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

