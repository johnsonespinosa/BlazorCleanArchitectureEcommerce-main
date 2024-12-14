using Application.Models;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<Response<Guid>>;
}
