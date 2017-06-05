using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Security;

namespace ForeignTradeContractsWorkstation.App.Reports.Helpers
{
    public static class DirectoryHelper
    {
        private static string _defaultPath = "dsf";


        public static string CreateDirectoryIfNotExist(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
            catch (Exception ex)
            {
                Directory.CreateDirectory(_defaultPath);
                return _defaultPath;
            }
        }

        public static string ValidateFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return Guid.NewGuid().ToString();
            }
            return fileName;
        }
    }
}
