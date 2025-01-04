using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table setup
            builder.ToTable("Categories");

            // Setting the primary key
            builder.HasKey(category => category.Id);

            // Setting the properties
            builder.Property(category => category.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Setting up the hierarchical relationship
            builder.HasOne(navigationExpression: category => category.Parent) // Relationship with the parent category
                .WithMany(navigationExpression: category => category.SubCategories) // Relationship with subcategories
                .HasForeignKey(category => category.ParentId) // Foreign key pointing to ParentId
                .OnDelete(DeleteBehavior.Restrict); // Behavior in case of deletion

            // Setting up the relationship with products
            builder.HasMany(navigationExpression: category => category.Products) // Relationship with products
                .WithOne(navigationExpression: product => product.Category) 
                .OnDelete(DeleteBehavior.Cascade); // Behavior in case of deletion

            // Auditable properties
            builder.Property(category => category.Created)
                .IsRequired();

            builder.Property(category => category.CreatedBy)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(category => category.LastModified)
                .IsRequired();

            builder.Property(category => category.LastModifiedBy)
                .HasMaxLength(50)
                .IsRequired(false);

            var electronics = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Electronics",
                ParentId = null,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var mobilePhones = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Mobile Phones",
                ParentId = electronics.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var laptops = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Laptops",
                ParentId = electronics.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var televisions = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Televisions",
                ParentId = electronics.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var homeAppliances = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Home Appliances",
                ParentId = null,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var refrigerators = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Refrigerators",
                ParentId = homeAppliances.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var washingMachines = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Washing Machines",
                ParentId = homeAppliances.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var furnitureAndDecor = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Furniture & Decor",
                ParentId = null,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var sofasAndChairs = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Sofas & Chairs",
                ParentId = furnitureAndDecor.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var tablesAndDesks = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Tables & Desks",
                ParentId = furnitureAndDecor.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var fashion = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Fashion",
                ParentId = null,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var menClothing = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Men's Clothing",
                ParentId = fashion.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var womenClothing = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Women's Clothing",
                ParentId = fashion.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var accessories = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Accessories",
                ParentId = fashion.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            var footwear = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Footwear",
                ParentId = fashion.Id,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTimeOffset.UtcNow,
                LastModifiedBy = "Seeder"
            };

            // Add the new data to the entity configuration
            builder.HasData(
               fashion, menClothing, womenClothing, accessories, footwear, electronics, mobilePhones, laptops, televisions,
               homeAppliances, refrigerators, washingMachines,
               furnitureAndDecor, sofasAndChairs, tablesAndDesks
            );
        }
    }
}
