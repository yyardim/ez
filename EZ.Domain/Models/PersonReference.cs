using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EZ.Domain.Models
{
    [Table("Person")]
    public class PersonReference
    {
        [Key]
        public int PersonID { get; set; }
        
        [StringLength(30)]
        public string FirstName { get; private set; }
        
        [StringLength(30)]
        public string MiddleName { get; private set; }
        
        [StringLength(30)]
        public string LastName { get; private set; }
        
        [StringLength(1)]
        public string Gender { get; private set; }
        
        public bool Active { get; private set; }

        [NotMapped]
        public string FullName
        {
            get { return ((FirstName) + (MiddleName == String.Empty ? String.Empty : MiddleName) + (" " + LastName)); }
        }
    }
}
