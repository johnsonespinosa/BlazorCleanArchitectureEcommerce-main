using Application.Models;

namespace Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Response<ProductResponse>>;
}
