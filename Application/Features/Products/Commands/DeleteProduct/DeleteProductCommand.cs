using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<Response<Guid>>;

    internal sealed class DeleteProductCommandHandler(IRepositoryAsync<Product> repository)
        : IRequestHandler<DeleteProductCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Product> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task<Response<Guid>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            // Get the product by ID
            var product = await _repository.GetByIdAsync(command.Id, cancellationToken);

            // Check if the product exists
            if (product is null)
                throw new NotFoundException(key: command.Id.ToString(), nameof(Product)); 

            // Delete the product through the repository
            await _repository.DeleteAsync(product, cancellationToken);

            // Create response with product ID removed
            var response = new Response<Guid>(command.Id);
            return response;
        }
    }
}