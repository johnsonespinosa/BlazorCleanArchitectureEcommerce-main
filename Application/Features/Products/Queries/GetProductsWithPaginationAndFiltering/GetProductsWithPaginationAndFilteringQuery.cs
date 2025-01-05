using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Products.Queries.GetProductsWithPaginationAndFiltering
{
    public record GetProductsWithPaginationAndFilteringQuery(FilterRequest FilterRequest) : IRequest<PaginatedResponse<ProductResponse>>;
    internal sealed class GetProductsWithPaginationAndFilteringQueryHandler(IProductService service) : IRequestHandler<GetProductsWithPaginationAndFilteringQuery, PaginatedResponse<ProductResponse>>
    {
        private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<PaginatedResponse<ProductResponse>> Handle(GetProductsWithPaginationAndFilteringQuery query, CancellationToken cancellationToken)
        {
            return await _service.GetAllProductsWithPaginationAsync(query.FilterRequest, cancellationToken);
        }
    }
}
