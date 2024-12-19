using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : IRequest<Response<IEnumerable<CategoryResponse>>> { }
    internal sealed class GetCategoriesQueryHandler(IRepositoryAsync<Category> repository, IMapper mapper) : IRequestHandler<GetCategoriesQuery, Response<IEnumerable<CategoryResponse>>>
    {
        public async Task<Response<IEnumerable<CategoryResponse>>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var specification = new GetCategoriesSpecificationQuery();
            var entities = await repository.ListAsync(specification, cancellationToken);
            var data = mapper.Map<IEnumerable<CategoryResponse>>(entities);
            var response =  new Response<IEnumerable<CategoryResponse>>(data);
            return response;
        }
    }
}
