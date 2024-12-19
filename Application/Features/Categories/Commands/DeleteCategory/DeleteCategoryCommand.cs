using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; init; }
    }
    internal sealed class DeleteCategoryCommandHandler(IRepositoryAsync<Category> repository) : IRequestHandler<DeleteCategoryCommand, Response<Guid>>
    {
        public async Task<Response<Guid>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(command.Id, cancellationToken);

            if(entity is null)
                throw new NotFoundException(command.Id.ToString(), nameof(Category));

            await repository.DeleteAsync(entity, cancellationToken);

            var response = new Response<Guid>(command.Id);
            return response;
        }
    }
}
