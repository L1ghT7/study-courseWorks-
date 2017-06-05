using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.Reports.base_classes;
using ForeignTradeContractsWorkstation.App.Reports.Helpers;
using SautinSoft.Document;
using SautinSoft.Document.Tables;

namespace ForeignTradeContractsWorkstation.App.Reports
{
    public  sealed  class InvoiceWordReport: BaseWordReport<Orders>
    {

        public InvoiceWordReport(string templateName)
            : base(templateName,null, @"WordTemplates","WordReports")
        {
         
        }

        public override string CreateReport(Orders viewModel)
        {
            ReplaceWith("{Name}",viewModel.Counterparties.full_name);
            ReplaceWith("{Address}", viewModel.Counterparties.legal_address);
            ReplaceWith("{Phones}", viewModel.Counterparties.telephones);
            ReplaceWith("{Name}", viewModel.Counterparties.full_name);
            ReplaceWith("{CheckingAccount}", viewModel.Counterparties.checking_account);
            ReplaceWith("{UNP}", viewModel.Counterparties.UNP);
            ReplaceWith("{Date}", DateTime.Now.ToShortDateString());


            var table = GetFirstTable();



            int totalAmount = 0;
            float? totalCost = 0;
            float? totalPrice = 0;
            float? totalVat = 0;

            float? totalVatSum = 0;
            float? totalTotalPrice = 0;
            decimal totalWeight = 0;

            var numberOfGoods = 1;

            foreach (var node in viewModel.Goods)
            {
                TableRow row = new TableRow(DOCX);
                AddCellToRow(row,numberOfGoods.ToString());
                AddCellToRow(row,node.Goods.full_name);
                AddCellToRow(row, "КГ.");
                AddCellToRow(row, node.TotalAmount.ToString());
                AddCellToRow(row,node.Goods.purchase_price.ToString());
                var price = (node.Goods.purchase_price*(float) node.TotalAmount);

                AddCellToRow(row, (price).ToString());
                AddCellToRow(row,node.Goods.VAT.ToString());
                var withVAT = ((node.Goods?.purchase_price.Value * (float)node.TotalAmount) / 100) * node.Goods.VAT;
                AddCellToRow(row,withVAT.ToString());
                AddCellToRow(row,(price+withVAT).ToString());
                table.Rows.Add(row);


                totalVatSum += withVAT;
                totalPrice += price;
                totalTotalPrice += (withVAT + price);

                numberOfGoods++;
            }
            
            ReplaceWith("{sum}", totalPrice.ToString());
            ReplaceWith("{sumVAT}", totalVatSum.ToString());
            ReplaceWith("{totalSum}", totalTotalPrice.ToString());

            var val = NumberToWordConverterHelper.ConvertCurrencyToWords((decimal)totalVatSum);
            ReplaceWith("{totalVatWord}", $"{val.Number} рублей {val.Coins} копеек");


            SaveReport();
            return FullDestinationPath;
        }
    }
}
