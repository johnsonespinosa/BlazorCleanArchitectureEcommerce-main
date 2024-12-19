using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Configuración de la tabla
            builder.ToTable("Categories");

            // Configuración de la clave primaria
            builder.HasKey(c => c.Id);

            // Configuración de las propiedades
            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Configuración de la relación jerárquica
            builder.HasOne(c => c.Parent) // Relación con la categoría padre
                .WithMany(c => c.SubCategories) // Relación con las subcategorías
                .HasForeignKey(c => c.ParentId) // Clave foránea que apunta a ParentId
                .OnDelete(DeleteBehavior.Restrict); // Comportamiento en caso de eliminación

            // Configuración de la relación con productos
            builder.HasMany(c => c.Products) // Relación con productos
                .WithOne(p => p.Category) 
                .OnDelete(DeleteBehavior.Cascade); // Comportamiento en caso de eliminación

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
