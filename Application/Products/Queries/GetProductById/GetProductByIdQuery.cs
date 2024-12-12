using Application.Models;
using MediatR;

namespace Application.UseCases.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse>;
}
