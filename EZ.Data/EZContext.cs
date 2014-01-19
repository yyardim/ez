using System.Data.Entity;
using EZ.Domain;
using System.Data.Entity.ModelConfiguration.Conventions;
using EZ.Data.SampleData;
using System.ComponentModel.DataAnnotations.Schema;

namespace EZ.Data
{
    public class EZContext : DbContext
    {
        //private string ezSchema = "EZ";

        public EZContext()
            : base(nameOrConnectionString:"EZContext") { }

        static EZContext()
        {
            Database.SetInitializer(new EZDatabaseInitializer());
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            
            modelBuilder.Entity<Address>().Property(a => a.DateModified).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            
            //modelBuilder.Configurations.Add(new AddressConfiguration());
            //modelBuilder.Configurations.Add(new EventerConfiguration());
            modelBuilder.Configurations.Add(new EzConfiguration());
            //modelBuilder.Configurations.Add(new PersonAddressMap());
            //modelBuilder.Configurations.Add(new PersonConfiguration());

            //modelBuilder.Entity<Eventers>()
            //    .HasKey(k => new { k.EventID, k.PersonId });

            //modelBuilder.Entity<PersonAddress>()
            //    .HasKey(k => new { k.PersonId, k.AddressId });

            //modelBuilder.Entity<Persons>()
            //    .HasMany(a => a.Eventers)
            //    .WithRequired()
            //    .HasForeignKey(a => a.PersonId);

            modelBuilder.Entity<Category>()
                .HasMany(a => a.ParentCategories)
                .WithMany(a => a.SubCategories);

            //modelBuilder.Entity<Ez>()
            //    .HasMany(a => a.Eventers)
            //    .WithRequired()
            //    .HasForeignKey(a => a.EventID);


            //modelBuilder.Entity<Persons>()
            //    .HasMany(a => a.PersonAddresses)
            //    .WithRequired()
            //    .HasForeignKey(a => a.PersonId);

            //modelBuilder.Entity<Address>()
            //    .HasMany(a => a.PersonAddresses)
            //    .WithRequired()
            //    .HasForeignKey(a => a.AddressId);

            //modelBuilder.Entity<Persons>()
            //    .HasMany(t => t.Ezs)
            //    .WithMany(t => t.Persons)
            //    .Map(t =>
            //    {
            //        t.ToTable("EventPerson");
            //        t.MapLeftKey("PersonId");
            //        t.MapRightKey("EventID");
            //    });

            //modelBuilder.Entity<Persons>()
            //    .HasMany(t => t.Addresses)
            //    .WithMany(t => t.Persons)
            //    .Map(t =>
            //    {
            //        t.ToTable("PersonAddress");
            //        t.MapLeftKey("PersonId");
            //        t.MapRightKey("AddressId");
            //    });

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Ez> Ezs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Eventer> Eventers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PersonAddress> PersonAddresses { get; set; }
    }
}