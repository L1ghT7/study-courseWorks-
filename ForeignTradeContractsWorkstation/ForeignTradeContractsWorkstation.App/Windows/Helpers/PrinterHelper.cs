using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace ForeignTradeContractsWorkstation.App.Windows.Helpers
{
    public static class PrinterHelper
    {
        public static void ShowExcel(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found,path: {path}");
            }
            var extension = Path.GetExtension(path);
            if (!new List<string>() { ".xls", ".xlsx" }.Contains(extension))
            {
                throw new FileFormatException($"The format '{extension}' is incorrect for excel file");
            }


            TaskFactory taskFactory = new TaskFactory();

            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb = excel.Workbooks.Open(path);
            excel.Visible = true;

        }

        public static void ShowWord(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found,path: {path}");
            }
            var extension = Path.GetExtension(path);
            if (!new List<string>() { ".docx", ".doc" }.Contains(extension))
            {
                throw new FileFormatException($"The format '{extension}' is incorrect for excel file");
            }


            TaskFactory taskFactory = new TaskFactory();

            Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document document = ap.Documents.Open(path);
            ap.Visible = true;

        }
    }
}
