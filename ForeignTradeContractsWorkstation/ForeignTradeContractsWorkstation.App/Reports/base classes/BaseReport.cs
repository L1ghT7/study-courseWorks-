using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Math;

namespace ForeignTradeContractsWorkstation.App.Reports.Helpers
{
    public abstract class BaseReport<TViewModel>
        where TViewModel : class, new()
    {
        protected string _destinatnionFolder, _fileName, _templateName, _templatesFolder, _basePath;
        protected string FullPathToTemplate => Path.Combine(_templatesFolder, _templateName);
        protected string FullDestinationPath => Path.Combine(_destinatnionFolder, _fileName);

        private BaseReport(string templatesFolder, string destinationFolder, string basePath)
        {
            _basePath = basePath ?? System.IO.Directory.GetCurrentDirectory().Replace(@"\bin\Debug", string.Empty);
            _templatesFolder = Path.Combine(_basePath, templatesFolder);
            _destinatnionFolder = Path.Combine(_basePath, destinationFolder);
        }


        protected BaseReport(string templateName,
          string basePath, string templatesFolder, string destinationFolder)
            : this(templatesFolder, destinationFolder,basePath)
        {
            _templateName = templateName;
            _fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture) + templateName;
            CheckIfTemplateDirectoryExist();
            CreateDestinationDirectoryIfNotExist();
        }


        protected void CheckIfTemplateDirectoryExist()
        {
            if (!File.Exists(FullPathToTemplate))
            {
                throw new FileNotFoundException($"Excel template file or directory doesn't exist with path {FullPathToTemplate}");
            }
        }

        protected void CreateDestinationDirectoryIfNotExist()
        {
            if (!Directory.Exists(_destinatnionFolder))
            {
                Directory.CreateDirectory(_destinatnionFolder);
            }
        }

        protected abstract void SaveReport();

        public abstract string CreateReport(TViewModel viewModel);

    }
}
