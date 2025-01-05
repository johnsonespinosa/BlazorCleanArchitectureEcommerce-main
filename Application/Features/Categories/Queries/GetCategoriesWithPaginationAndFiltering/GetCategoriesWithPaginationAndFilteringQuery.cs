using Application.Commons.Models;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategoriesWithPaginationAndFiltering
{
    public record GetCategoriesWithPaginationAndFilteringQuery : IRequest<PaginatedResponse<CategoryResponse>>
    {
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
        public string? TextFilter { get; init; }
    }
    internal sealed class GetCategoriesWithPaginationAndFilteringQueryHandler(
        IRepositoryAsync<Category> repository,
        IMapper mapper)
        : IRequestHandler<GetCategoriesWithPaginationAndFilteringQuery, PaginatedResponse<CategoryResponse>>
    {
        private readonly IRepositoryAsync<Category> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<PaginatedResponse<CategoryResponse>> Handle(GetCategoriesWithPaginationAndFilteringQuery query, CancellationToken cancellationToken)
        {
            // Create specification to get categories with pagination and filtering
            var specification = new GetCategoriesWithPaginationAndFilteringSpecificationQuery(query.PageNumber, query.PageSize, query.TextFilter);
    
            // Get the list of categories
            var categories = await _repository.ListAsync(specification, cancellationToken);
            
            // Count the total number of categories (for pagination)
            var totalCount = await _repository.CountAsync(specification, cancellationToken);
    
            // Map categories to CategoryResponse
            var mappedCategories = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
    
            // Create and return the paginated response
            return new PaginatedResponse<CategoryResponse>(items: mappedCategories.ToList(), totalCount, query.PageNumber, query.PageSize);
        }
    }
}
