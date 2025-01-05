using Application.Commons.Models;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.UpdateProduct;

namespace Application.Commons.Interfaces
{
    public interface IProductService
    {
        Task<Response<Guid>> CreateProductAsync(CreateProductCommand command, CancellationToken cancellationToken);
        Task<Response<ProductResponse>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PaginatedResponse<ProductResponse>> GetAllProductsWithPaginationAsync(FilterRequest filter, CancellationToken cancellationToken);
        Task<Response<IEnumerable<ProductResponse>>> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<Response<Guid>> UpdateProductAsync(UpdateProductCommand command, CancellationToken cancellationToken);
        Task<Response<Guid>> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
    }
}