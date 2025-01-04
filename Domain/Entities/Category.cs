using Domain.Commons;

namespace Domain.Entities
{
    public sealed class Category : BaseAuditableEntity
    {
        private readonly ICollection<Category> _subCategories = new List<Category>();
        private readonly ICollection<Product> _products = new List<Product>();
        
        /// <summary>
        /// Required name for the category
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Property to establish the hierarchical relationship
        /// </summary>
        public Guid? ParentId { get; init; }
        
        /// <summary>
        /// Navigation to parent category
        /// </summary>
        public Category? Parent { get; init; } 
        
        /// <summary>
        /// Collection of products
        /// </summary>
        public IReadOnlyCollection<Product> Products => (IReadOnlyCollection<Product>)_products;
        
        /// <summary>
        /// Collection of subcategories
        /// </summary>
        public IReadOnlyCollection<Category> SubCategories => (IReadOnlyCollection<Category>)_subCategories;
    }
}
