using FluentValidation;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .Length(1, 100).WithMessage("El nombre de la categoría debe tener entre 1 y 100 caracteres.");
        }
    }
}
