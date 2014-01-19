using System.Data.Entity.ModelConfiguration;

namespace EZ.Domain
{
    public class EzConfiguration : EntityTypeConfiguration<Ez>
    {
        public EzConfiguration()
        {
            //Primary Key
            //this.HasKey(t => t.EventID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(75);

            this.Property(t => t.Description)
                .HasMaxLength(2500);

            // Relationships
            //below explanation is: Ez which has an AddressId foreignKey in it, requires an address, which might have many events attached to it.
            //this.HasRequired(t => t.Address)
            //    .WithMany(t => t.Ezs)
            //    .HasForeignKey(d => d.AddressId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
