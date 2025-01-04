namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(expression: command => command.CategoryId)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(expression: command => command.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(1, 100).WithMessage("The product name must be between 1 and 100 characters.");

            RuleFor(expression: command => command.Description)
                .MaximumLength(500).WithMessage("The description cannot exceed 500 characters.");

            RuleFor(expression: command => command.ImageUrl)
                .Must(BeAValidUrl).WithMessage("The image URL is invalid.");

            RuleFor(expression: command => command.Price)
                .GreaterThan(valueToCompare: 0).WithMessage("The price must be greater than zero.");

            RuleFor(expression: command => command.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");
        }

        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
