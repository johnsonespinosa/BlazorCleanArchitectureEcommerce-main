using Domain.Commons;

namespace Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string? Name { get; set; }

        /// <summary>
        /// Propiedad para establecer la relación jerárquica
        /// </summary>
        public Guid ParentId { get; set; }
        public Category? Parent { get; set; } // Navegación hacia la categoría padre
        public ICollection<Product>? Products { get; set; }
        public ICollection<Category>? SubCategories { get; set; } // Colección de subcategorías
    }
}
