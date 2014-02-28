using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EZ.Domain;
using EZ.Data;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace EZ.Data
{
    /// <summary>
    /// The EZ-dependent, generic repository for data acess
    /// </summary>
    /// <typeparam name="T">Type of entity for this EzRepository.</typeparam>
    public class EzRepository<T> : IRepository<T> where T : class
    {
        #region Attributes
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }
        #endregion

        #region CTOR

        public EzRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        #endregion

        #region Methods
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T GetById(long id)
        {
            return DbSet.Find(id);
        }

        public virtual T GetById(string id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }            
        }

        public virtual void Delete(long id)
        {
            var entity = GetById(id);
            if (entity == null) return; //not found
            Delete(entity);
        }
        #endregion

        #region Methods Secondary
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = DbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                DbSet.Remove(obj);
            }
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).FirstOrDefault<T>();
        }
        #endregion
    }
}
