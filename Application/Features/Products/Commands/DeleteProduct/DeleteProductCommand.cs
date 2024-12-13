using Application.Models;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<WritingResponse>;
}
