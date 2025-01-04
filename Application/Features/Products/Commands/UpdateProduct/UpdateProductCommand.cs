using Application.Interfaces;
using Application.Models;
using Domain.Entities;

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

    internal sealed class UpdateProductCommandHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<UpdateProductCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Product> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<Guid>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            // Get the product by ID
            var product = await _repository.GetByIdAsync(command.Id, cancellationToken);

            // Check if the product exists
            if (product is null)
                throw new NotFoundException(key: command.Id.ToString(), nameof(Product));

            // Map the changes from the command to the existing entity
            _mapper.Map(command, product);

            // Update the product in the repository
            await _repository.UpdateAsync(product, cancellationToken);

            // Create response with updated product ID
            var response = new Response<Guid>(command.Id);
            return response;
        }
    }
}