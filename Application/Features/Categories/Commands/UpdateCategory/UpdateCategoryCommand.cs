using Application.Commons.Models;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, Guid? ParentId, string Name) : IRequest<Response<Guid>>;

    internal sealed class UpdateCategoryCommandHandler(IRepositoryAsync<Category> repository, IMapper mapper)
        : IRequestHandler<UpdateCategoryCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Category> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<Guid>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            // Get the existing category
            var category = await _repository.GetByIdAsync(command.Id, cancellationToken);

            // Check if the category exists
            if (category is null)
                return new Response<Guid>(message: ResponseMessages.EntityNotFound);

            // Map the changes from the command to the existing entity
            _mapper.Map(command, category);

            // Update the category via the repository
            await _repository.UpdateAsync(category, cancellationToken);

            // Create response with the updated category ID
            var response = new Response<Guid>(data: command.Id, message: ResponseMessages.EntityUpdated);
            return response;
        }
    }
}