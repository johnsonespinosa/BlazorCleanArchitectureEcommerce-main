using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Response<Guid>>;

    internal sealed class DeleteCategoryCommandHandler(ICategoryService service) : IRequestHandler<DeleteCategoryCommand, Response<Guid>>
    {
        private readonly ICategoryService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<Response<Guid>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            return await _service.DeleteCategoryAsync(command.Id, cancellationToken);
        }
    }
}