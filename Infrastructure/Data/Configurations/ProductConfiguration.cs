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

            builder.HasKey(p => p.Id);

            builder.Property(p => p.CategoryId)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Stock)
                .IsRequired();

            // Propiedades auditables
            builder.Property(p => p.Created)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(p => p.LastModified)
                .IsRequired();

            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(50)
                .IsRequired(false);

            // Relación con Category
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
