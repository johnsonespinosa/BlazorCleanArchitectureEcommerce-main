using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProducts
{
    public sealed class GetProductsSpecificationQuery : Specification<Product>
    {
        public GetProductsSpecificationQuery()
        {
            Query.Where(product => product.IsDeleted == false)
                .Include(product => product.Category);
        }
    }
}
