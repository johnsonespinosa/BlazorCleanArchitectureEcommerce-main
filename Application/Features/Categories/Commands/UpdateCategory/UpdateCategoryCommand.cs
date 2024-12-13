using Application.Features.Categories.Commands.CreateCategory;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id) : CreateCategoryCommand;
}
