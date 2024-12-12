using Application.Models;
using MediatR;

namespace Application.UseCases.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<WritingResponse>;
}
