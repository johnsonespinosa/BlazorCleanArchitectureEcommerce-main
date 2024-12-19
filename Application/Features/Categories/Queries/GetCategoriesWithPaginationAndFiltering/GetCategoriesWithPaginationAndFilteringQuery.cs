using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategoriesWithPaginationAndFiltering
{
    public record GetCategoriesWithPaginationAndFilteringQuery : IRequest<PaginatedResponse<CategoryResponse>>
    {
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
        public string? Filter { get; init; }
    }
    internal sealed class GetCategoriesWithPaginationAndFilteringQueryHandler(IRepositoryAsync<Category> repository, IMapper mapper) : IRequestHandler<GetCategoriesWithPaginationAndFilteringQuery, PaginatedResponse<CategoryResponse>>
    {
        public async Task<PaginatedResponse<CategoryResponse>> Handle(GetCategoriesWithPaginationAndFilteringQuery query, CancellationToken cancellationToken)
        {
            var specification = new GetCategoriesWithPaginationAndFilteringSpecificationQuery(query.PageNumber, query.PageSize, query.Filter!);
            var entities = await repository.ListAsync(specification, cancellationToken);
            var response = mapper.Map<PaginatedResponse<CategoryResponse>>(entities);
            return response;
        }
    }
}
