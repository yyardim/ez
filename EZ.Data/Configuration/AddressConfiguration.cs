using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EZ.Domain
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.AddressId);

            // Properties
            this.Property(t => t.AddressLine1)
                .HasMaxLength(75);

            this.Property(t => t.AddressLine2)
                .HasMaxLength(75);

            this.Property(t => t.AddressLine3)
                .HasMaxLength(75);

            this.Property(t => t.City)
                .HasMaxLength(75);

            this.Property(t => t.PostalCode)
                .HasMaxLength(10);

            this.Property(t => t.StateOrProvinceCode)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.CountryCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            // Table & Column Mappings
            //this.ToTable("Address","EZ");
            //this.Property(t => t.AddressId).HasColumnName("AddressId");
            //this.Property(t => t.AddressType).HasColumnName("AddressTypeID");
            //this.Property(t => t.AddressLine1).HasColumnName("AddressLine1");
            //this.Property(t => t.AddressLine2).HasColumnName("AddressLine2");
            //this.Property(t => t.AddressLine2).HasColumnName("AddressLine3");
            //this.Property(t => t.City).HasColumnName("City");
            //this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            //this.Property(t => t.StateOrProvinceCode).HasColumnName("StateOrProvinceCode");
            //this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            //this.Property(t => t.GeoLocation).HasColumnName("GeoLocation");
            //this.Property(t => t.DateModified).HasColumnName("DateModified");
        }
    }
}
