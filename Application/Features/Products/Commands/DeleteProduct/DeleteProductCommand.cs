using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; init; }
    }
    internal sealed class DeleteProductCommandHandler(IRepositoryAsync<Product> repository)
        : IRequestHandler<DeleteProductCommand, Response<Guid>>
    {
        public async Task<Response<Guid>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(command.Id, cancellationToken);

            if (entity is null)
                throw new NotFoundException(command.Id.ToString(), nameof(Category));

            await repository.DeleteAsync(entity, cancellationToken);

            var response = new Response<Guid>(command.Id);
            return response;
        }
    }
}
