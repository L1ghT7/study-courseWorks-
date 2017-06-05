using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.Reports.Helpers;
using SautinSoft.Document;
using SautinSoft.Document.Tables;

namespace ForeignTradeContractsWorkstation.App.Reports.base_classes
{
    public abstract class BaseWordReport<TViewModel> : BaseReport<TViewModel>
        where TViewModel: class,new()
    {

        protected DocumentCore DOCX;

        protected BaseWordReport(string templateName, string basePath, string templatesFolder, string destinationFolder) 
            : base(templateName, basePath, templatesFolder, destinationFolder)
        {
            DOCX = DocumentCore.Load(FullPathToTemplate);
        }

        protected void ReplaceWith(string selector,string value)
        {
            foreach (ContentRange text in DOCX.Content.Find(selector).Reverse())
            {
                text.Replace(value);
                
            }
        }

        protected Table GetFirstTable()
        {
            return (Table)DOCX.GetChildElements(true, ElementType.Table).First();
        }

        protected override void SaveReport()
        {
            DOCX.Save(FullDestinationPath);
        }

        protected void AddCellToRow(TableRow row, string value)
        {
            var tableCell = new TableCell(DOCX);
            tableCell.Content.Start.Insert(value);
            row.Cells.Add(tableCell);
        }
    }
}
