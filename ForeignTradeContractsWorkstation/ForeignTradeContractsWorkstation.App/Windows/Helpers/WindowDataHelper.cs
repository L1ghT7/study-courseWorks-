using ForeignTradeContractsWorkstation.App.Database;
using ForeignTradeContractsWorkstation.App.Database.Contexts;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.windows.interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ForeignTradeContractsWorkstation.App.Windows.Custom_Attributes;

namespace ForeignTradeContractsWorkstation.App.Windows.Helpers
{
    public static class WindowDataHelper
    {
        private readonly static string _updateMessage = "Выберите запись для изменения!",
            _deleteMessage = "Выберите запись для удаления!",
            _errorCaption = "Ошибка",
            _errorUpdateMessage = "Ошибка при обновлении записи. Причина:",
            _successUpdateMessage = "Запись успешно изменена!",
            _successDeleteMessage = "Запись успешно удалена!",
            _successAddMessage = "Запись успешно добавлена!",
            _deleteWarningMessage = "Будут удалены записи из связанных таблиц:";

        public static void AddOrdUpdateRecord<T>(T record)
            where T : class, IBaseEntity
        {
            var status = DirectoryContextHelper.AddOrUpdate(record);
            if (status == Enums.EntityStatus.Added)
            {
                MessageBox.Show(_successAddMessage, string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (status == Enums.EntityStatus.Updated)
            {
                MessageBox.Show(_successUpdateMessage, string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void DeleteRecord<T>(T record, params string[] withValues)
            where T : class, IBaseEntity
        {
            var status = DirectoryContextHelper.Delete(record,withValues);
            if (status == Enums.EntityStatus.Deleted)
            {
                MessageBox.Show(_successDeleteMessage, string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static ObservableCollection<T> GetAllRecords<T>(params string[] withValues)
            where T : class, IBaseEntity
        {
            return DirectoryContextHelper.GetAllEntities<T>(withValues);
        }

        public static void UpdateDataGridRecord<T>(object entity, Func<T, IBaseWindowContract> instance)
            where T : class
        {
            var castedEntity = entity as T;
            if (castedEntity == null)
            {
                MessageBox.Show(_updateMessage, _errorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var updateForm = instance(castedEntity);
                updateForm.ShowDialog();
            }
        }
        public static void DeleteDataGridRecord<T>(T entity, bool warningParentDelete = false, params string[] withValues)
            where T : class, IBaseEntity
        {
            if (entity == null)
            {
                MessageBox.Show(_deleteMessage, _errorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (warningParentDelete)
                {
                    var dialogResult = MessageBox.Show($"{_deleteWarningMessage} {GetParentTable<T>()}", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (dialogResult == MessageBoxResult.OK)
                    {
                        DeleteRecord(entity,withValues);
                    }
                }
                else
                {
                    DeleteRecord(entity,withValues);
                }
            }
        }
        public static void FillListboxWithDatagrid(DataGrid datagrid, ListBox listBox)
        {
            string text = string.Empty;
            var selectedItem = datagrid.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            listBox.Items.Clear();
            foreach (var column in datagrid.Columns)
            {
                var data = (datagrid.SelectedCells[column.DisplayIndex].Column.GetCellContent(selectedItem)
                    as TextBlock).Text;
                listBox.Items.Add(string.Format("{0}: {1}", column.Header, data));
            }
        }


        public static T AddFullGraph<T>(T record)
            where T : class, IBaseEntity
        {
            var added = DirectoryContextHelper.AddFullGraph(record);
            if (added != null)
            {
                MessageBox.Show(_successAddMessage, string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return added;
        }

        public static T UpdateFullGraph<T>(T record)
            where T : class,IBaseEntity
        {
            try
            {
                var updated = DirectoryContextHelper.UpdateFullGraph(record);
                MessageBox.Show(_successUpdateMessage, string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
                return updated;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{_errorUpdateMessage} {ex.Message}",_errorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        } 


        private static string GetParentTable<T>()
            where T : class, IBaseEntity
        {
            var tables = new StringBuilder();
            var entityType = typeof(T);
            var virtualProperties = entityType.GetProperties().Where(x => x.GetMethod.IsVirtual);

            var withDisplatName = virtualProperties.
                Where(
                    x =>
                        x.PropertyType.IsGenericType
                            ? x.PropertyType.GetGenericArguments()[0].GetCustomAttribute(typeof(TableNameAttribute)) !=
                              null
                            : x.PropertyType.GetCustomAttribute(typeof(TableNameAttribute)) != null)
                .Select(
                    x =>
                        x.PropertyType.IsGenericType
                            ? (x.PropertyType.GetGenericArguments()[0].GetCustomAttribute(typeof(TableNameAttribute)) as TableNameAttribute).TableName
                            : (x.PropertyType.GetCustomAttribute(typeof(TableNameAttribute)) as TableNameAttribute).TableName);

            return string.Join(",", withDisplatName.Select(x=>x.ToLowerInvariant()));
        }
    }
}
