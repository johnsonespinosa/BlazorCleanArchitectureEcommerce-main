using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategories
{
    /// <summary>
    /// Specification for getting categories that have not been deleted.
    /// </summary>
    public sealed class GetCategoriesSpecificationQuery : Specification<Category>
    {
        public GetCategoriesSpecificationQuery()
        {
            // Filter categories that are not marked as deleted
            Query.Where(category => !category.IsDeleted)
                .Include(category => category.SubCategories) // Include related subcategories
                .Include(category => category.Products); // Include related products
        }
    }
}