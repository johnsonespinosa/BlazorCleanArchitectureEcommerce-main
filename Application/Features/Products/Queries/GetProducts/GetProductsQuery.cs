using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery : IRequest<Response<IEnumerable<ProductResponse>>> { }
    internal sealed class GetProductsQueryHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<GetProductsQuery, Response<IEnumerable<ProductResponse>>>
    {
        public async Task<Response<IEnumerable<ProductResponse>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var specification = new GetProductsSpecificationQuery();
            var entities = await repository.ListAsync(specification, cancellationToken);
            var data = mapper.Map<IEnumerable<ProductResponse>>(entities);
            var response = new Response<IEnumerable<ProductResponse>>(data);
            return response;
        }
    }
}
