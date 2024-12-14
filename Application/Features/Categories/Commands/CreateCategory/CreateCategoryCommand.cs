using Application.Models;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand : IRequest<Response<Guid>>
    {
        public string? Name { get; init; }
    }
}
