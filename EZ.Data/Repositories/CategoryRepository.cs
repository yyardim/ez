using EZ.Data;
using EZ.Data.Contracts;
using EZ.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Data
{
    public class CategoryRepository : EZRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context) { }

        public IQueryable<Category> GetEventsOfCategory(long categoryId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetSubCategories(long categoryId)
        {
            //TODO: This logic is wrong
            //var category = DbSet.Where(c => c.CategoryId == categoryId);
            //var subCategories = category.
            return DbSet.Where(ec => ec.CategoryId == categoryId);
        }

        public IQueryable<Category> GetParentCategories(long categoryId)
        {
            return DbSet.Where(ec => ec.CategoryId == categoryId);
        }
    }
}
