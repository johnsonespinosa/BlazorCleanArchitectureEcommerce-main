using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        Guid CategoryId,
        string Name,
        string Description,
        string ImageUrl,
        decimal Price,
        int Stock) : IRequest<Response<Guid>>;

    internal sealed class UpdateProductCommandHandler(IProductService service) : IRequestHandler<UpdateProductCommand, Response<Guid>>
    {
        private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<Response<Guid>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            return await _service.UpdateProductAsync(command, cancellationToken);
        }
    }
}