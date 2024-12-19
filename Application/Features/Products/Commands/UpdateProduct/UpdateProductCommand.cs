using Application.Features.Products.Commands.CreateProduct;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand : CreateProductCommand
    {
        public Guid Id { get; init; }
    }
    internal sealed class UpdateProductCommandHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<UpdateProductCommand, Response<Guid>>
    {
        public async Task<Response<Guid>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(command.Id, cancellationToken);

            if (entity is null)
                throw new NotFoundException(command.Id.ToString(), nameof(Product));

            mapper.Map(command, entity);

            await repository.UpdateAsync(entity, cancellationToken);

            var response = new Response<Guid>(command.Id);
            return response;
        }
    }
}
