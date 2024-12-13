using Application.Models;
using MediatR;

namespace Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse>;
}
