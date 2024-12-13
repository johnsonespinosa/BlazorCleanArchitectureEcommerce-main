using Application.Models;
using MediatR;

namespace Application.UseCases.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<WritingResponse>;
}
