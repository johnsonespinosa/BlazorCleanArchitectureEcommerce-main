using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategoriesWithPaginationAndFiltering
{
    public class GetCategoriesWithPaginationAndFilteringSpecificationQuery : Specification<Category>
    {
        public GetCategoriesWithPaginationAndFilteringSpecificationQuery(int pageNumber, int pageSize, string? filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
                Query.Where(category => category.Name!.Contains(filter));

            Query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Include(category => category.Products);
        }
    }
}
