using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using DocumentFormat.OpenXml.Spreadsheet;
using ForeignTradeContractsWorkstation.App.Reports.Helpers;
using iTextSharp.text.pdf.draw;
using SpreadsheetLight;
using static iTextSharp.text.Font;

namespace ForeignTradeContractsWorkstation.App.Reports.base_classes
{
    public abstract class BaseExcelReport<TViewModel>: BaseReport<TViewModel>
        where TViewModel : class,new()
    {
        private const string DEFAULT_TEMPLATE = @"ExcelTemplates\TTN",
            DEFAULT_DESTINATION = "ExcelReports";

        protected int CellOffset { get; set; }
        protected abstract SLDocument sl { get; set; }
        protected string SheetName;

        protected BaseExcelReport(string templateName,string sheetName = "first",
            string basePath = null,string templatesFolder = DEFAULT_TEMPLATE,string destinationFolder = DEFAULT_DESTINATION)
            :base(templateName,basePath,templatesFolder,destinationFolder)
        {
            SheetName = sheetName;
        }

        protected IEnumerable<int> AddDynamicRows(int start,int count)
        {
            sl.InsertRow(start+1, count-1);
            int i;
            for (i = start; i < start+count-1; i++)
            {
                sl.CopyRow(start, i+1, false);
                yield return i;
            }
            yield return i;
            CellOffset = count - 1;
        } 

        protected virtual void SetValue(string column,int row,string value)
        {
            //var d = sl.GetCellStyle($"{column}{row + CellOffset}");
            //d.Alignment = new SLAlignment();
            //d.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            sl.SetCellValue($"{column}{row + CellOffset}", value);
        }

        protected override void SaveReport()
        {
            sl.SaveAs(FullDestinationPath);
        }

   
        protected string GetMoneyString(float? number)
        {
            return number.ToString();
        }

        protected string GetWithPrecent(float? number)
        {
            return $"{number} %";
        }

        protected virtual string GetFormatedDateAndTime(DateTime? date, TimeSpan? time)
        {
            if (!date.HasValue || !time.HasValue)
                return string.Empty;

            var formattedDate = date.Value.Date.ToString(@"MM\.dd");
            var formattedTime = time.Value.ToString(@"hh\:mm");

            return $"{formattedTime} {formattedDate}";
        }
    }
}
