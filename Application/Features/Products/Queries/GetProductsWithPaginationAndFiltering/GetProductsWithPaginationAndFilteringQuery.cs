using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProductsWithPaginationAndFiltering
{
    public record GetProductsWithPaginationAndFilteringQuery : IRequest<PaginatedResponse<ProductResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? Filter { get; init; }
    }
    internal sealed class GetProductsWithPaginationAndFilteringQueryHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<GetProductsWithPaginationAndFilteringQuery, PaginatedResponse<ProductResponse>>
    {
        public async Task<PaginatedResponse<ProductResponse>> Handle(GetProductsWithPaginationAndFilteringQuery query, CancellationToken cancellationToken)
        {
            var specification = new GetProductsWithPaginationAndFilteringSpecificationQuery(query.PageNumber, query.PageSize, query.Filter);
            var entities = await repository.ListAsync(specification, cancellationToken);
            var response = mapper.Map<PaginatedResponse<ProductResponse>>(entities);
            return response;
        }
    }
}
