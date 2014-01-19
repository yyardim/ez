using EZ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Data.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IQueryable<Category> GetEventsOfCategory(long categoryId);
        IQueryable<Category> GetSubCategories(long categoryId);
        IQueryable<Category> GetParentCategories(long categoryId);
    }
}
