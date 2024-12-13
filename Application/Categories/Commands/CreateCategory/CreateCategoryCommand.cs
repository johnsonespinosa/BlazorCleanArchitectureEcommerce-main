using Application.Models;
using MediatR;

namespace Application.UseCases.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand : IRequest<WritingResponse>
    {
        public string? Name { get; init; }
    }
}
