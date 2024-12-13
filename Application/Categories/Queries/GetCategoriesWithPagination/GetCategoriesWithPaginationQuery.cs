using Application.Models;
using MediatR;

namespace Application.UseCases.Categories.Queries.GetCategoriesWithPagination
{
    public record GetCategoriesWithPaginationQuery(FilterRequest Filter) : IRequest<PaginatedResponse<CategoryResponse>>;
}
