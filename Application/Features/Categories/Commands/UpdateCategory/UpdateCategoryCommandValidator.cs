using FluentValidation;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(expression: command => command.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .Length(1, 100).WithMessage("The category name must be between 1 and 100 characters.");
        }
    }
}
