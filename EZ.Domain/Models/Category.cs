using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EZ.Domain
{
    [Table("Category", Schema = "ez")]
    public class Category
    {
        private ICollection<Category> _parentCategories; 
        private ICollection<Category> _subCategories;
        private ICollection<Ez> _ezs;

        public Category()
        {
            _parentCategories = new List<Category>();
            _subCategories = new List<Category>();
            _ezs = new List<Ez>();
        }
        //public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateModified { get; set; }

        public virtual ICollection<Category> ParentCategories
        {
            get { return _parentCategories; }
            set { _parentCategories = value; }
        }
        public virtual ICollection<Category> SubCategories 
        {
            get { return _subCategories; }
            set { _subCategories = value; }
        }
        public virtual ICollection<Ez> Ezs 
        {
            get { return _ezs; }
            set { _ezs = value; }
        }
    }
}
