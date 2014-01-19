using System.Data.Entity.ModelConfiguration;

namespace EZ.Domain
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            // Primary Key
            HasKey(t => t.PersonId);

            //Properties
            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(75);

            Property(t => t.MiddleName)
                .HasMaxLength(75);

            Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(75);

            Property(t => t.PasswordHash)
                .IsRequired()
                .HasMaxLength(128);

            Property(t => t.Biography)
                .HasMaxLength(2500);

            Property(t => t.Gender)
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.ImageSource)
                .HasMaxLength(250);                

            // Relationships            
        }
    }
}
