using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Categories.Queries.GetCategoriesWithPaginationAndFiltering
{
    public record GetCategoriesWithPaginationAndFilteringQuery(FilterRequest FilterRequest)
        : IRequest<PaginatedResponse<CategoryResponse>>;
    internal sealed class GetCategoriesWithPaginationAndFilteringQueryHandler(ICategoryService service) : IRequestHandler<GetCategoriesWithPaginationAndFilteringQuery, PaginatedResponse<CategoryResponse>>
    {
        private readonly ICategoryService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<PaginatedResponse<CategoryResponse>> Handle(GetCategoriesWithPaginationAndFilteringQuery query, CancellationToken cancellationToken)
        {
            return await _service.GetAllCategoriesWithPaginationAsync(query.FilterRequest, cancellationToken);
        }
    }
}
