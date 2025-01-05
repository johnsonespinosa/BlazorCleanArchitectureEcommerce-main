using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<Response<Guid>>;

    internal sealed class DeleteProductCommandHandler(IProductService service) : IRequestHandler<DeleteProductCommand, Response<Guid>>
    {
        private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<Response<Guid>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            return await _service.DeleteProductAsync(command.Id, cancellationToken);
        }
    }
}