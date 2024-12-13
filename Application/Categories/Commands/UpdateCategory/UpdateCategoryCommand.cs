using Application.UseCases.Categories.Commands.CreateCategory;

namespace Application.UseCases.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id) : CreateCategoryCommand;
}
