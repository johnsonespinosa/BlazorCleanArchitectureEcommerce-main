using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery : IRequest<Response<IEnumerable<ProductResponse>>> { }

    internal sealed class GetProductsQueryHandler(IProductService service) : IRequestHandler<GetProductsQuery, Response<IEnumerable<ProductResponse>>>
    {
        private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<Response<IEnumerable<ProductResponse>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            return await _service.GetAllProductsAsync(cancellationToken);
        }
    }
}