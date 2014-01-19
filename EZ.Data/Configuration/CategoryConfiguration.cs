using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EZ.Domain
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            // Primary Key
            HasKey(t => t.CategoryId);
            //Property(t => t.CategoryId)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(60);

            Property(t => t.Description)
                .HasMaxLength(500);

            HasMany(t => t.SubCategories)
                .WithMany(t => t.ParentCategories)
                .Map(m => m
                    .ToTable("CategoryHierarchy", "EZ")
                    .MapRightKey("CategoryId")
                    .MapLeftKey("ParentCategoryId")
                );

            //HasMany(t => t.ParentCategories)
            //    .WithMany(t => t.SubCategories)
            //    .Map(m => m
            //        .ToTable("CategoryHierarchy","EZ")
            //        .MapLeftKey("CategoryId")
            //        .MapRightKey("ParentCategoryId")
            //    );

            HasMany(t => t.Ezs)
                .WithMany(t => t.Categories)
                .Map(m =>m
                    .ToTable("CategoryEvent","EZ")
                    .MapLeftKey("CategoryId")
                    .MapRightKey("EventId")
                );
        }
    }
}
