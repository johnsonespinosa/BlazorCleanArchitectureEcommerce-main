using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(
        Guid CategoryId,
        string Name,
        string Description,
        string ImageUrl,
        decimal Price,
        int Stock) : IRequest<Response<Guid>>;
    internal sealed class CreateProductCommandHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<CreateProductCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Product> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        public async Task<Response<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Mapping command to entity
            var entity = _mapper.Map<Product>(command);
            
            // Add the category via the repository
            var product = await _repository.AddAsync(entity, cancellationToken);
            
            // Create response with the ID of the new category
            var response = new Response<Guid>(product.Id);
            return response;
        }
    }
}
