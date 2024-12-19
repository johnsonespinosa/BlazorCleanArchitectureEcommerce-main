using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand : IRequest<Response<Guid>>
    {
        public string? Name { get; init; }
    }

    internal sealed class CreateCategoryCommandHandler(IRepositoryAsync<Category> repository, IMapper mapper) : IRequestHandler<CreateCategoryCommand, Response<Guid>>
    {
        public async Task<Response<Guid>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Category>(command);
            var data = await repository.AddAsync(entity, cancellationToken);
            var response = new Response<Guid>(data.Id);
            return response;
        }
    }
}
