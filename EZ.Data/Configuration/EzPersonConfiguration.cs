using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Domain
{
    public class EzPersonConfiguration : EntityTypeConfiguration<EzPerson>
    {
        public EzPersonConfiguration()
        {
            //Composite key
            //this.HasKey(t => new { t.EventID, t.PersonID });

            //// Table & Column Mappings
            //this.Property(t => t.IsHost).HasColumnName("IsHost");
            //this.Property(t => t.IsInvited).HasColumnName("IsInvited");
            //this.Property(t => t.IsGoing).HasColumnName("IsGoing");
            //this.Property(t => t.IsNotGoing).HasColumnName("IsNotGoing");
            //this.Property(t => t.IsMaybe).HasColumnName("IsMaybe");
            //this.Property(t => t.Guest).HasColumnName("Guest");
            //this.Property(t => t.IsHost).HasColumnName("IsHost");
            //this.Property(t => t.HasJoined).HasColumnName("HasJoined");
            //this.Property(t => t.HasCreated).HasColumnName("HasCreated");
            //this.Property(t => t.DateModified).HasColumnName("DateModified");

            // Relationships
            //EzPersons has one person, persons have many ezPersons records
            //this.HasRequired(t => t.Persons)
            //    .WithMany(t => t.EzPersons)
            //    .HasForeignKey(t => t.PersonId)
            //    .WillCascadeOnDelete(false);

            //EzPersons has 1 Ez, events have many eventpeople records
            //this.HasRequired(t => t.Ez)
            //    .WithMany(t => t.EzPersons)
            //    .HasForeignKey(t => t.EventID)
            //    .WillCascadeOnDelete(false);
        }
        
    }
}
