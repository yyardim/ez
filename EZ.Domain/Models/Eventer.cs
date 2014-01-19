using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EZ.Domain
{
    [Table("EventPerson", Schema = "EZ")]
    public class Eventer
    {
        [Key, Column(Order = 0)]
        public long PersonId { get; set; }

        [Key, Column(Order = 1)]
        public long EzId { get; set; }

        [ForeignKey("PersonId")]
        public virtual  Person Person { get; set; }

        [ForeignKey("EzId")]
        public virtual Ez Ez { get; set; }
        
        public bool IsHost { get; set; }
        public bool IsInvited { get; set; }
        public bool IsGoing { get; set; }
        public bool IsNotGoing { get; set; }
        public bool IsMaybe { get; set; }
        public Int16 Guest { get; set; }
        public bool HasJoined { get; set; }
        public bool HasCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateModified { get; set; }
    }
}
