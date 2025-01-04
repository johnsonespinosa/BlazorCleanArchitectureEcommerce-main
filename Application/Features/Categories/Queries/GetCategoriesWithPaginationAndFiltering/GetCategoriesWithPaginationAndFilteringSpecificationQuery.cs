using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategoriesWithPaginationAndFiltering
{
    public sealed class GetCategoriesWithPaginationAndFilteringSpecificationQuery : Specification<Category>
    {
        public GetCategoriesWithPaginationAndFilteringSpecificationQuery(int pageNumber, int pageSize, string? filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
                Query.Where(category => category.Name!.Contains(filter));

            Query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            
            // Filter categories that are not marked as deleted
            Query.Where(category => !category.IsDeleted)
                .Include(category => category.SubCategories) // Include related subcategories
                .Include(category => category.Products); // Include related products
        }
    }
}
