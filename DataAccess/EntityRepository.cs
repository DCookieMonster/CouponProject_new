using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace DataAccess
{
    public class EntityRepository<T> 
     where T : class, new()
    {

        readonly DbContext _entitiesContext;

        public EntityRepository(DbContext entitiesContext)
        {

            if (entitiesContext == null)
            {

                throw new ArgumentNullException("entitiesContext");
            }

            _entitiesContext = entitiesContext;
        }

        //...

        public virtual void Add(T entity)
        {
            _entitiesContext.Entry(entity).State = (EntityState) System.Data.EntityState.Added;
            Save();
        }

        public virtual void Edit(T entity)
        {

            _entitiesContext.Entry(entity).State = (EntityState) System.Data.EntityState.Modified;
            Save();
        }

        public virtual void Delete(T entity)
        {

            _entitiesContext.Entry(entity).State = (EntityState) System.Data.EntityState.Deleted;
            Save();
        }

        private bool Save()
        {
            return _entitiesContext.SaveChanges() > 0;
        }
    }
}