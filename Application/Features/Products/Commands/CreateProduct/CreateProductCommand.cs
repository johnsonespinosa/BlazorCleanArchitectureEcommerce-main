using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(
        Guid CategoryId,
        string Name,
        string Description,
        string ImageUrl,
        decimal Price,
        int Stock) : IRequest<Response<Guid>>;
    internal sealed class CreateProductCommandHandler(IProductService service) : IRequestHandler<CreateProductCommand, Response<Guid>>
    {
        private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));
        public async Task<Response<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            return await _service.CreateProductAsync(command, cancellationToken);
        }
    }
}
