using Application.Features.Products.Commands.CreateProduct;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id) : CreateProductCommand;
}
