using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProductsWithPaginationAndFiltering
{
    public sealed class GetProductsWithPaginationAndFilteringSpecificationQuery : Specification<Product>
    {
        public GetProductsWithPaginationAndFilteringSpecificationQuery(int pageNumber, int pageSize, string? filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
                Query.Where(product => product.Name!.Contains(filter) || product.Description!.Contains(filter));

            Query.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Where(product => !product.IsDeleted)
                .Include(product => product.Category);
        }
    }
}
