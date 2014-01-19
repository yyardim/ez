using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Domain
{
    public class PersonAddresConfiguration : EntityTypeConfiguration<PersonAddress>
    {
        public PersonAddresConfiguration()
        {
            this.HasKey(t => new { PersonID = t.PersonId, AddressID = t.AddressId });

            // Relationships
            ////PersonAddress has one person, persons have many personaddress records
            //this.HasRequired(t => t.Persons)
            //    .WithMany(t => t.PersonAddresses)
            //    .HasForeignKey(t => t.PersonId)
            //    .WillCascadeOnDelete(false);

            //PersonAddress has one Adress, address have many personAddress records
            //this.HasRequired(t => t.Address)
            //    .WithMany(t => t.PersonAddresses)
            //    .HasForeignKey(t => t.AddressId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
