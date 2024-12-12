using Application.Models;
using MediatR;

namespace Application.UseCases.Products.Queries.GetProductsWithPagination
{
    public record GetProductsWithPaginationQuery(FilterRequest Filter) : IRequest<PaginatedResponse<ProductResponse>>;
}
