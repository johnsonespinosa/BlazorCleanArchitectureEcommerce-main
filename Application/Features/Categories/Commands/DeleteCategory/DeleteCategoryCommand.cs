using Application.Commons.Models;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Response<Guid>>;

    internal sealed class DeleteCategoryCommandHandler(IRepositoryAsync<Category> repository)
        : IRequestHandler<DeleteCategoryCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Category> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task<Response<Guid>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            // Get the category by ID
            var category = await _repository.GetByIdAsync(command.Id, cancellationToken);

            // Check if the category exists
            if (category is null)
                return new Response<Guid>(message: ResponseMessages.EntityNotFound);

            // Delete the category via the repository
            await _repository.DeleteAsync(category, cancellationToken);

            // Create response with the ID of the deleted category
            var response = new Response<Guid>(data: command.Id, message: ResponseMessages.EntityDeleted);
            return response;
        }
    }
}