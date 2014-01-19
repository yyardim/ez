using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EZ.Domain
{
    [Table("PersonAddress", Schema = "EZ")]
    public class PersonAddress
    {
        [Key, Column(Order = 0)]
        public long PersonId { get; set; }
        [Key, Column(Order = 1)]
        public long AddressId { get; set; }
        
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        
        public bool IsDefault { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateModified { get; set; }
    }
}
