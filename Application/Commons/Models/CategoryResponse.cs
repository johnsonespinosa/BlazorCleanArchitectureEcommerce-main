using Application.Models;

namespace Application.Commons.Models
{
    /// <summary>
    /// DTO to represent the response of a category.
    /// </summary>
    public class CategoryResponse
    {
        private readonly ICollection<CategoryResponse> _subCategories = new List<CategoryResponse>();
        private readonly ICollection<ProductResponse> _products = new List<ProductResponse>();

        /// <summary>
        /// Unique identifier of the category.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Parent identifier, if any.
        /// </summary>
        public Guid? ParentId { get; init; }

        /// <summary>
        /// Category name.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Collection of products associated with this category.
        /// </summary>
        public IReadOnlyCollection<ProductResponse> Products => (IReadOnlyCollection<ProductResponse>)_products;
        
        /// <summary>
        /// Collection of subcategories associated with this category.
        /// </summary>
        public IReadOnlyCollection<CategoryResponse> SubCategories => (IReadOnlyCollection<CategoryResponse>)_subCategories;
    }
}