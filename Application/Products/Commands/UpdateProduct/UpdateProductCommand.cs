using Application.UseCases.Products.Commands.CreateProduct;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id) : CreateProductCommand;
}
