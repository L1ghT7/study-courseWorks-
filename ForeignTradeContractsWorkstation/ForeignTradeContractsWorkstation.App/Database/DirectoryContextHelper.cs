using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using ForeignTradeContractsWorkstation.App.Database.Contexts;
using ForeignTradeContractsWorkstation.App.Windows.Helpers.Enums;

namespace ForeignTradeContractsWorkstation.App.Database
{
    public static class DirectoryContextHelper
    {
        private static readonly long DEFAULT_ID = 0;

        public static EntityStatus AddOrUpdate<T>(T entity)
            where T : class, IBaseEntity
        {
            var status = EntityStatus.Unchange;
            using (var context = new Directory())
            {
                var dbSet = context.Set<T>();
                if (entity.Id != DEFAULT_ID)
                {
                    dbSet.Attach(entity);
                    context.Entry(entity).State = EntityState.Modified;
                    status = EntityStatus.Updated;
                }
                else
                {
                    dbSet.Attach(entity);
                    context.Entry(entity).State = EntityState.Added;
                    status = EntityStatus.Added;
                }
                context.SaveChanges();
            }
            return status;
        }

        public static T AddFullGraph<T>(T entity)
         where T : class, IBaseEntity
        {
            using (var context = new Directory())
            {
                var dbSet = context.Set<T>();
                var added = dbSet.Add(entity);
                context.SaveChanges();
                return added;
            }
        }

        public static T UpdateFullGraph<T>(T entity)
            where T : class, IBaseEntity
        {
            T updatedEntity = null;
            using (var context = new Directory())
            {
                var dbSet = context.Set<T>();
                var entity1 = dbSet.Find(entity.Id);
                dbSet.Remove(entity1);
                updatedEntity = dbSet.Add(entity);
                context.SaveChanges();
            }
            return updatedEntity;

        }


        public static EntityStatus Delete<T>(T entity, params string[] includeProperties)
            where T : class, IBaseEntity
        {
            var status = EntityStatus.Unchange;
            using (var context = new Directory())
            {
                var dbSet = context.Set<T>();
                if (entity.Id != DEFAULT_ID)
                {
                    IQueryable<T> queryable = dbSet;
                    foreach (var property in includeProperties)
                    {
                        queryable = queryable.Include(property);
                    }

                    var entityToDelete = queryable.FirstOrDefault(x => x.Id == entity.Id);
                    if (entityToDelete != null)
                    {
                        dbSet.Remove(entityToDelete);
                        status = EntityStatus.Deleted;
                    }
                }
                context.SaveChanges();
            }
            return status;
        }

        public static ObservableCollection<T> GetAllEntities<T>(params string[] includeProperties)
            where T : class, IBaseEntity
        {
            using (var context = new Directory())
            {
                var dbSet = context.Set<T>();
                IQueryable<T> queryable = dbSet;
                foreach (var property in includeProperties)
                {
                    queryable = queryable.Include(property);
                }
                var list = queryable.ToList();
                return new ObservableCollection<T>(list);
            }
        }
    }
}
