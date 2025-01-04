using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(product => product.Id);

            builder.Property(product => product.CategoryId)
                .IsRequired();

            builder.Property(product => product.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(product => product.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(product => product.ImageUrl)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(product => product.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(product => product.Stock)
                .IsRequired();

            // Auditable properties
            builder.Property(product => product.Created)
                .IsRequired();

            builder.Property(product => product.CreatedBy)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(product => product.LastModified)
                .IsRequired();

            builder.Property(product => product.LastModifiedBy)
                .HasMaxLength(50)
                .IsRequired(false);

            // Relationship with Category
            builder.HasOne(navigationExpression: product => product.Category)
                .WithMany(navigationExpression: category => category.Products)
                .HasForeignKey(product => product.CategoryId);
        }
    }
}
