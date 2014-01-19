using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace EZ.Domain
{
    [Table("Address", Schema = "EZ")]
    public partial class Address
    {
        private ICollection<Ez> _ezs;
        private ICollection<PersonAddress> _personAddresses;

        public Address()
        {
            _ezs = new List<Ez>();
            _personAddresses = new List<PersonAddress>();
        }

        public long AddressId { get; set; }
        public Enums.AddressType AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StateOrProvinceCode { get; set; }
        public string CountryCode { get; set; }
        public DbGeography GeoLocation { get; set; }
        public DateTime? DateCreated { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateModified { get; set; }
        //[NotMapped]
        //public State State { get; set; }

        public virtual ICollection<Ez> Ezs 
        {
            get { return _ezs; }
            set { _ezs = value; }
        }
        public virtual ICollection<PersonAddress> PersonAddresses
        {
            get { return _personAddresses; }
            set { _personAddresses = value; }
        }
    }
}
