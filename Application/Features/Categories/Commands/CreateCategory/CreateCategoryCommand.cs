using Application.Commons.Models;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(Guid? ParentId, string Name) : IRequest<Response<Guid>>;

    internal sealed class CreateCategoryCommandHandler(IRepositoryAsync<Category> repository, IMapper mapper)
        : IRequestHandler<CreateCategoryCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Category> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<Guid>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            // Mapping command to entity
            var entity = _mapper.Map<Category>(command);

            // Add the category via the repository
            var category = await _repository.AddAsync(entity, cancellationToken);

            // Create response with the ID of the new category
            var response = new Response<Guid>(data: category.Id, message: ResponseMessages.EntityCreated);
            return response;
        }
    }
}