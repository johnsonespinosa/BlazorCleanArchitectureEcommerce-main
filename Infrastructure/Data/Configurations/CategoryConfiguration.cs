using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Propiedades auditables
            builder.Property(c => c.Created)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(c => c.LastModified)
                .IsRequired();

            builder.Property(c => c.LastModifiedBy)
                .HasMaxLength(50)
                .IsRequired(false);
        }
    }
}
