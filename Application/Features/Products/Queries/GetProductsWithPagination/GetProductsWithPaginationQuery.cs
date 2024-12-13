using Application.Models;
using MediatR;

namespace Application.Features.Products.Queries.GetProductsWithPagination
{
    public record GetProductsWithPaginationQuery(PaginationRequest Pagination) : IRequest<PaginatedResponse<ProductResponse>>;
}
