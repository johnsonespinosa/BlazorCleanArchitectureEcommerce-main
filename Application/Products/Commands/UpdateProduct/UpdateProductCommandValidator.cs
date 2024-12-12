using FluentValidation;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.CategoryId)
                .NotEmpty().WithMessage("La categoría es obligatorio.");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .Length(1, 100).WithMessage("El nombre del producto debe tener entre 1 y 100 caracteres.");

            RuleFor(command => command.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");

            RuleFor(command => command.ImageUrl)
                .Must(BeAValidUrl).WithMessage("La URL de la imagen no es válida.");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");

            RuleFor(command => command.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
        }

        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
