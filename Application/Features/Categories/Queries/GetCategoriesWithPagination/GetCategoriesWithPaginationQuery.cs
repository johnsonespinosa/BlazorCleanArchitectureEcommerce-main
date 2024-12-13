using Application.Models;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoriesWithPagination
{
    public record GetCategoriesWithPaginationQuery(FilterRequest Filter) : IRequest<PaginatedResponse<CategoryResponse>>;
}
