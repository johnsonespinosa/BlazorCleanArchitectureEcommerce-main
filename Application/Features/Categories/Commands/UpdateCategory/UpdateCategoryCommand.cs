using Application.Features.Categories.Commands.CreateCategory;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand : CreateCategoryCommand
    {
        public Guid Id { get; init; }
    }
    internal sealed class UpdateCategoryCommandHandler(IRepositoryAsync<Category> repository, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, Response<Guid>>
    {
        public async Task<Response<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null)
                throw new NotFoundException(request.Id.ToString(), nameof(Category));

            mapper.Map(request, entity);

            await repository.UpdateAsync(entity, cancellationToken);

            var response = new Response<Guid>(request.Id);
            return response;
        }
    }
}
