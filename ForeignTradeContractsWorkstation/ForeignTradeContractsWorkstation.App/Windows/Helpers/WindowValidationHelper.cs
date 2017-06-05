using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml;
using ForeignTradeContractsWorkstation.App.Database.Contexts;

namespace ForeignTradeContractsWorkstation.App.Windows.Helpers
{
    public static class WindowValidationHelper
    {
        public static bool ValidateEntity<T>(T entity)
            where T : class, IBaseEntity
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var result = !properties.Where(x=>!x.GetGetMethod().IsVirtual).Any((x) =>
           {
               var value = x.GetValue(entity);
               return x.PropertyType == typeof(string) &&
                         string.IsNullOrEmpty((string)value);
           });
            if (!result)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
