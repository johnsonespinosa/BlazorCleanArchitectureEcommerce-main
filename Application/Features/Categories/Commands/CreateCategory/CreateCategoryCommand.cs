using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(Guid? ParentId, string Name) : IRequest<Response<Guid>>;

    internal sealed class CreateCategoryCommandHandler(ICategoryService service) : IRequestHandler<CreateCategoryCommand, Response<Guid>>
    {
        private readonly ICategoryService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<Response<Guid>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            return await _service.CreateCategoryAsync(command, cancellationToken);
        }
    }
}