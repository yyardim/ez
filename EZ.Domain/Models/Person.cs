using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EZ.Domain
{
    [Table("Person", Schema = "EZ")]
    public class Person
    {
        private ICollection<PersonAddress> _personAddresses;
        private ICollection<Eventer> _eventers;
        
        public Person()
        {
            _personAddresses = new List<PersonAddress>();
            _eventers = new List<Eventer>();
        }

        [Key]
        public long PersonId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public bool RememberMe { get; set; }
        public string ImageSource { get; set; }
        public string Biography { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateModified { get; set; }

        //[NotMapped]
        //public State State { get; set; }
        public virtual ICollection<Eventer> Eventers 
        {
            get { return _eventers; }
            set { _eventers = value; }
        }
        public virtual ICollection<PersonAddress> PersonAddresses 
        {
            get { return _personAddresses; }
            set { _personAddresses = value; }
        }
    }
}
