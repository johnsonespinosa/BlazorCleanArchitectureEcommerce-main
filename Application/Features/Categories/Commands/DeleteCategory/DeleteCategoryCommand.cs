using Application.Models;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Response<Guid>>;
}
