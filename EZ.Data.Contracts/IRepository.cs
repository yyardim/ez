using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(long Id);
        T GetById(string Id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(long Id);

        void Delete(Expression<Func<T, bool>> where);
        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
    }
}
