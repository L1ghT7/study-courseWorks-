using ForeignTradeContractsWorkstation.App.Database;
using ForeignTradeContractsWorkstation.App.Database.Contexts;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignTradeContractsWorkstation.App.Windows.Helpers
{
    public static class WindowSearchHelper
    {
        public static IEnumerable<T> Search<T>(string value, params string[] withProperties)
            where T : class,IBaseEntity
        {
            if (value == string.Empty) return WindowDataHelper.GetAllRecords<T>(withProperties);
            using (var context = new Directory())
            {
                var dbset = context.Set<T>();
                var filtered = dbset.ToList().Where((entity) => SearchPredicate(entity, value)).ToList();
                return filtered;
            }
        }

        private static bool SearchPredicate<T>(T entity, string searchValue)
            where T : class
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (!property.GetAccessors()[0].IsVirtual)
                {
                    var propertyValue = property.GetValue(entity)?.ToString();
                    if (propertyValue != null)
                        if (propertyValue.ToUpperInvariant().Contains(searchValue.ToUpperInvariant())) { return true; }
                }
            }
            return false;
        }
    }
}
