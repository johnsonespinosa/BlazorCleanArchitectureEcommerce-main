using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesSpecificationQuery : Specification<Category>
    {
        public GetCategoriesSpecificationQuery()
        {
            Query.Where(category => category.IsDeleted == false).Include(category => category.Products);
        }
    }
}
