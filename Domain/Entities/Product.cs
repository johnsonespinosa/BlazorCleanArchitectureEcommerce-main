using Domain.Commons;

namespace Domain.Entities
{
    public sealed class Product : BaseAuditableEntity
    {
        /// <summary>
        /// Unique identifier for the category this product belongs to.
        /// </summary>
        public Guid CategoryId { get; init; }

        /// <summary>
        /// Navigation property for the associated category.
        /// </summary>
        public Category? Category { get; init; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// URL of the product image.
        /// </summary>
        public string? ImageUrl { get; init; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; init; }

        /// <summary>
        /// Available stock for the product.
        /// </summary>
        public int Stock { get; init; }
    }
}