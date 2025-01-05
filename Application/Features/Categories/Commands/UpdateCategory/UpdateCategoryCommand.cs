using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, Guid? ParentId, string Name) : IRequest<Response<Guid>>;

    internal sealed class UpdateCategoryCommandHandler(ICategoryService service) : IRequestHandler<UpdateCategoryCommand, Response<Guid>>
    {
        private readonly ICategoryService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<Response<Guid>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            return await _service.UpdateCategoryAsync(command, cancellationToken);
        }
    }
}