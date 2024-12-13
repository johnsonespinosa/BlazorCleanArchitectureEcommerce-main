using FluentValidation;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .Length(1, 100).WithMessage("El nombre de la categoría debe tener entre 1 y 100 caracteres.");
        }
    }
}
