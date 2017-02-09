using SoftBabymartVn.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SoftBabymartVn.Infractstructure
{
    public class CRUD
    {
        private babymart_vnEntities DbContext = new babymart_vnEntities();
        public virtual void Update<T>(T entity, params Expression<Func<T, object>>[] updatedProperties) where T : class
        {
            //dbEntityEntry.State = EntityState.Modified; --- I cannot do this.

            //Ensure only modified fields are updated.
            var dbEntityEntry = DbContext.Entry(entity);

            DbContext.Set<T>().Attach(entity);
            if (updatedProperties.Any())
            {
                //update explicitly mentioned properties
                foreach (var property in updatedProperties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                //no items mentioned, so find out the updated entries
                //foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
                //{
                //    var original = dbEntityEntry.OriginalValues.GetValue<object>(property);
                //    var current = dbEntityEntry.CurrentValues.GetValue<object>(property);
                //    if (original != null && !original.Equals(current))
                //        dbEntityEntry.Property(property).IsModified = true;
                //}
                DbContext.Entry(entity).State = EntityState.Modified;

            }
        }
        public virtual void Add<T>(T entity) where T : class
        {
            DbContext.Set<T>().Add(entity);
        }
        public virtual void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}