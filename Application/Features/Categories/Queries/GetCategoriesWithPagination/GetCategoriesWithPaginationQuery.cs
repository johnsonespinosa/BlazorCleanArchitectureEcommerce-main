using Application.Models;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoriesWithPagination
{
    public record GetCategoriesWithPaginationQuery(PaginationRequest Pagination) : IRequest<PaginatedResponse<CategoryResponse>>;
}
