using Application.Models;
using MediatR;

namespace Application.Features.Products.Queries.GetProductsWithPagination
{
    public record GetProductsWithPaginationQuery(FilterRequest Filter) : IRequest<PaginatedResponse<ProductResponse>>;
}
