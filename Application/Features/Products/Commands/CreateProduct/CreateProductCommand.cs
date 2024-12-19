using Application.Interfaces;
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
    internal sealed class CreateProductCommandHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<CreateProductCommand, Response<Guid>>
    {
        public async Task<Response<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Product>(command);
            var data = await repository.AddAsync(entity, cancellationToken);
            var response = new Response<Guid>(data.Id);
            return response;
        }
    }
}
