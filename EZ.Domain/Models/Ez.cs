using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EZ.Domain
{
    [Table("Event", Schema= "EZ")]
    public class Ez
    {
        private ICollection<Eventer> _eventers;
        private ICollection<Category> _categories;

        public Ez()
        {
            _eventers = new List<Eventer>();
            _categories = new List<Category>();
        }

        [Key]
        public long EzId { get; set; }
        public long CategoryId { get; set; }
        
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        
        public bool IsActive { get; set; }
        public bool IsCheckInRequired { get; set; }
        public bool IsPublic { get; set; }
        public short? MaxGuests { get; set; }
        public DateTime? DateCreated { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateModified { get; set; }
        //[NotMapped]
        //public State State { get; set; }
        
        public virtual ICollection<Eventer> Eventers 
        {
            get { return _eventers; }
            set { _eventers = value; }
        }

        public virtual ICollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }
    }
}