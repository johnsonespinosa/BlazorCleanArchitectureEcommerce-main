using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<Response<Guid>>
    {
        public Guid CategoryId { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? ImageUrl { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
    }
}
