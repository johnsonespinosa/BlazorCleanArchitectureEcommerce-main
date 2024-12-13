using Application.Models;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand : IRequest<WritingResponse>
    {
        public string? Name { get; init; }
    }
}
