using Application.Commons.Models;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProductsWithPaginationAndFiltering
{
    public record GetProductsWithPaginationAndFilteringQuery : IRequest<PaginatedResponse<ProductResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? Filter { get; init; }
    }
    internal sealed class GetProductsWithPaginationAndFilteringQueryHandler(
        IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<GetProductsWithPaginationAndFilteringQuery, PaginatedResponse<ProductResponse>>
    {
        private readonly IRepositoryAsync<Product> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<PaginatedResponse<ProductResponse>> Handle(GetProductsWithPaginationAndFilteringQuery query, CancellationToken cancellationToken)
        {
            // Create specification to get products with pagination and filtering
            var specification = new GetProductsWithPaginationAndFilteringSpecificationQuery(query.PageNumber, query.PageSize, query.Filter);
        
            // Get the list of products from the repository
            var products = await _repository.ListAsync(specification, cancellationToken);
        
            // Count the total products for pagination
            var totalCount = await _repository.CountAsync(specification, cancellationToken);
        
            // Map entities to DTOs
            var mappedProducts = _mapper.Map<IEnumerable<ProductResponse>>(products);
        
            // Create paginated response
            return new PaginatedResponse<ProductResponse>(items: mappedProducts.ToList(), totalCount, query.PageNumber, query.PageSize);
        }
    }
}
